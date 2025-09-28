namespace ct.backend.Features.Auth
{
    public sealed class RefreshRequest
    {
        public string RefreshToken { get; set; } = default!;
    }

}
