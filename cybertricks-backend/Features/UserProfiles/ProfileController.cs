using ct.backend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ct.backend.Features.UserProfiles
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // yêu cầu login
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private Task<User?> GetCurrentUserAsync() => _userManager.GetUserAsync(User);

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var user = await GetCurrentUserAsync();
            if (user is null) return Unauthorized(new { message = "Not logged in" });

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                id = user.Id,
                email = user.Email,
                fullName = user.FullName,
                firstName = user.FirstName,
                lastName = user.LastName,
                avatarUrl = user.AvatarUrl,
                subscriptionType = user.SubscriptionType,
                subscriptionStartDate = user.SubscriptionStartDate,
                subscriptionEndDate = user.SubscriptionEndDate,
                isActive = user.IsActive,
                createdAt = user.CreatedAt,
                updatedAt = user.UpdatedAt,
                lastLogin = user.LastLogin,
                roles
            });
        }
        [HttpPut("update")]
        [Authorize]
        [RequestSizeLimit(10_000_000)] // giới hạn 10MB
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileRequest request, IFormFile? avatarFile)
        {
            var user = await GetCurrentUserAsync();
            if (user is null) return Unauthorized(new { message = "Not logged in" });

            user.FirstName = request.FirstName ?? user.FirstName;
            user.LastName = request.LastName ?? user.LastName;

            // nếu có file mới thì lưu lại
            if (avatarFile != null && avatarFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(avatarFile.FileName);
                var savePath = Path.Combine("wwwroot", "assets", "images", "avt", fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await avatarFile.CopyToAsync(stream);
                }

                // Lưu path relative để FE load
                user.AvatarUrl = $"assets/images/avt/{fileName}";
            }

            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { message = "Update failed", errors = result.Errors });

            return Ok(new { message = "Profile updated successfully", avatarUrl = user.AvatarUrl });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = await GetCurrentUserAsync();
            if (user is null) return Unauthorized(new { message = "Not logged in" });

            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest(new { message = "Passwords do not match" });

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                return BadRequest(new { message = "Change password failed", errors = result.Errors });

            // ❌ JWT là stateless, không có cookie sign-in để "refresh".
            // ✅ Nếu cần, hãy yêu cầu FE lấy token mới (refresh-token flow) sau khi đổi mật khẩu.
            return Ok(new { message = "Password changed successfully" });
        }
    }
}