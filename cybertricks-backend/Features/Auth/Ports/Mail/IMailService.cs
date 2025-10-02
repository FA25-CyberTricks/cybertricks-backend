namespace ct.backend.Features.Auth.Ports.Mail
{
    public interface IMailService
    {
        Task SendMailAsync(MailContent mailContent);
    }
}
