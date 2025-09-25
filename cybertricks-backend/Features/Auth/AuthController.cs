using ct.backend.Domain.Entities;
using ct.backend.Features.Accounts.Ports.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ct.backend.Features.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IMailService _mailService;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AuthController> logger,
            IMailService mailService,
            IConfiguration config,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mailService = mailService;
            _config = config;
            _env = env;
        }

        /// <summary>
        /// LOGIN (trả JWT)
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // 1) Tìm user theo username/email
            var user = await _userManager.FindByNameAsync(request.UserName)
                       ?? await _userManager.FindByEmailAsync(request.UserName);

            // Luôn trả Unauthorized cho thông tin sai để tránh lộ dữ liệu
            if (user is null)
                return Unauthorized(new { message = "Sai username hoặc password" });

            // 2) Check password (có lockout)
            var pwResult = await _signInManager.CheckPasswordSignInAsync(
                user, request.Password, lockoutOnFailure: true);

            if (!pwResult.Succeeded)
                return Unauthorized(new { message = "Sai username hoặc password" });

            // 3) (Tuỳ chọn) yêu cầu email đã confirm
            if (!await _userManager.IsEmailConfirmedAsync(user))
                return Unauthorized(new { message = "Email chưa được xác nhận" });

            // 4) Tạo JWT (dùng helper để thống nhất)
            var jwt = await GenerateJwtAsync(user);

            // 5) Trả về token + expires + roles
            var handler = new JwtSecurityTokenHandler();
            var parsed = handler.ReadJwtToken(jwt);

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                token = jwt,
                expires = parsed.ValidTo,
                roles
            });
        }

        /// <summary>
        /// REGISTER
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // 1) Check tồn tại
            var existed = await _userManager.FindByEmailAsync(request.Email);
            if (existed != null)
                return BadRequest(new { message = "UserName hoặc Email đã tồn tại" });

            // 2) Tạo user
            Random random = new Random();
            var user = new User
            {
                UserName = request.Email.Split('@')[0]
                    + $"{random.Next(0, 10)}{random.Next(0, 10)}{random.Next(0, 10)}{random.Next(0, 10)}",
                FullName = request.Fullname,
                Email = request.Email,
                EmailConfirmed = false
            };

            var create = await _userManager.CreateAsync(user, request.Password);
            if (!create.Succeeded)
                return BadRequest(create.Errors);

            // (optional) gán role mặc định
             await _userManager.AddToRoleAsync(user, "User");

            // 3) Tạo token confirm + encode an toàn cho URL
            var rawToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(rawToken));

            // 4) Build absolute confirm URL (giống MVC: Url.Action + Request.Scheme)
            var confirmUrl = Url.Action(
                action: nameof(ConfirmEmail),
                controller: "Auth",
                values: new { userId = user.Id, token = encodedToken, returnUrl = request.ReturnUrl },
                protocol: Request.Scheme
            );

            // 5) Load template & thay placeholder
            var emailBody = await BuildEmailBodyAsync(
                templateFileName: "ConfirmEmailTemplate.html",
                confirmationUrl: confirmUrl!,
                email: request.Email
            );

            // 6) Gửi mail
            await _mailService.SendMailAsync(new MailContent
            {
                To = request.Email,
                Subject = "Confirm Email CyberTricks",
                Body = emailBody
            });

            _logger.LogInformation("User registered, confirmation email sent.");

            return Ok(new
            {
                message = "Đăng ký thành công. Vui lòng kiểm tra email để xác nhận tài khoản.",
                confirmUrl // giữ để dev debug; prod có thể bỏ
            });
        }

        /// <summary>
        /// CONFIRM EMAIL
        /// </summary>
        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token, [FromQuery] string? returnUrl = null)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return BadRequest(new { message = "User không tồn tại" });

            // Decode token về dạng raw
            var decodedBytes = WebEncoders.Base64UrlDecode(token);
            var rawToken = Encoding.UTF8.GetString(decodedBytes);

            var result = await _userManager.ConfirmEmailAsync(user, rawToken);
            if (!result.Succeeded)
                return BadRequest(new { message = "Xác nhận email thất bại", errors = result.Errors });

            // Option 1: Redirect về FE nếu có returnUrl (SPA/FE site)
            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            // Option 2: Trả JSON
            return Ok(new { message = "Xác nhận email thành công" });
        }

        /// <summary>
        /// Helpers
        /// </summary>
        private async Task<string> BuildEmailBodyAsync(string templateFileName, string confirmationUrl, string email)
        {
            var templatePath = Path.Combine(_env.ContentRootPath, "Common", "Templates", templateFileName);

            var html = await System.IO.File.ReadAllTextAsync(templatePath);
            return html
                .Replace("{{ .ConfirmationURL }}", confirmationUrl)
                .Replace("{{ .Email }}", email);
        }

        private async Task<string> GenerateJwtAsync(User user)
        {
            // Lấy claims cơ bản + roles
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
        };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            // Ký JWT
            var secret = _config["Jwt:Key"];
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiresMinutes = int.TryParse(_config["Jwt:ExpiresMinutes"], out var m) ? m : 60;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
