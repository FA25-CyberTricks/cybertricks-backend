namespace ct.backend.Features.Accounts.Ports.Mail
{
    public interface IMailService
    {
        Task SendMailAsync(MailContent mailContent);
    }
}
