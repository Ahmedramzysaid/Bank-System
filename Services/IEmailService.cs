using System.Threading.Tasks;

namespace BankSystem.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body, string attachmentPath = null);
    }
}
