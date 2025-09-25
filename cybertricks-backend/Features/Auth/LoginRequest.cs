namespace ct.backend.Features.Accounts
{
    public class LoginRequest
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? TwoFactorCode { get; set; }
        public string? TwoFactorRecoveryCode { get; set; }
    }
}
