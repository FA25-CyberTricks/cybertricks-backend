namespace ct_backend.Features.Accounts
{
    public class ConfirmEmailRequest
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
    }
}
