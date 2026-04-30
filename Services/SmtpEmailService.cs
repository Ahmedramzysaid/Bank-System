using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace BankSystem.Services
{
    public class SmtpEmailService : IEmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body, string attachmentPath = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("bank.official.system@gmail.com"));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            if (!string.IsNullOrEmpty(attachmentPath) && System.IO.File.Exists(attachmentPath))
            {
                bodyBuilder.Attachments.Add(attachmentPath);
            }
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            
            try
            {
                // Example using Gmail's SMTP server
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                
                // IMPORTANT: Replace with actual credentials
                // For Gmail, you need an "App Password" if 2FA is enabled
                await smtp.AuthenticateAsync("bank.official.system@gmail.com", "lmtm narh riek jxyy");
                
                await smtp.SendAsync(email);
            }
            catch (System.Exception ex)
            {
                // For now, just print the error if auth fails so the app doesn't crash completely
                System.Console.WriteLine($"\n[Email Warning] Failed to send email: {ex.Message}");
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
