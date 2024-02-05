using API_Users.Models;

namespace API_Users.Services
{
    public interface IMailService
    {
        Task SendMailAsync(MailRequest mailRequest);
    }
}
