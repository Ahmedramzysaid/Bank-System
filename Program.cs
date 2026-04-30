using BankSystem.DbContexts;
using BankSystem.Services;
using BankSystem.UI;
using Microsoft.EntityFrameworkCore;

namespace BankSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new ApplicationDbContext();
            // Ensure database is created and up-to-date
            context.Database.Migrate();

            var emailService = new SmtpEmailService();
            var service = new BankService(context, emailService);
            var ui = new ConsoleUI(service);

            await ui.RunAsync();
        }
    }
}
