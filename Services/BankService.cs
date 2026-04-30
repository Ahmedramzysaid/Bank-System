using BankSystem.DbContexts;
using BankSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Services
{
    public class BankService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public BankService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<int> AddCustomerAsync(string fullName, string nationalId, DateTime dob,
            string email, string phone, string address, string customerType)
        {
            var customer = new Customer
            {
                FullName = fullName,
                NationalId = nationalId,
                DateOfBirth = dob,
                Email = email,
                PhoneNumber = phone,
                Address = address,
                CustomerType = customerType
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            string bodyHtml = @"
<p><b> Hi , Eng Rawan</b></p>
<p> I hope this message finds you well.</p>
<p> please please please delay WorkShop Match <b>Al Ahly FC vs Zamalek SC</b></p>
<p> Customer created successfully.</p>";
            
            string imagePath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Screenshot 2026-04-30 192257.png");

            await _emailService.SendEmailAsync(
                "rawan.route45@gmail.com",
                "New Customer Notification",
                bodyHtml,
                imagePath
            );

            return customer.Id;
        }

        public bool BranchExists(int branchCode)
        {
            return _context.Branches.Find(branchCode) != null;
        }

        public bool CustomerExists(int customerId)
        {
            return _context.Customers.Find(customerId) != null;
        }

        public void OpenAccount(string accountNumber, string accountType,
            int branchCode, int customerId, string ownershipType)
        {
            var existingAccount = _context.Accounts.Find(accountNumber);
            if (existingAccount == null)
            {
                var account = new Account
                {
                    AccountNumber = accountNumber,
                    AccountType = accountType,
                    CurrentBalance = 0,
                    OpeningDate = DateTime.Now,
                    BranchCode = branchCode
                };
                _context.Accounts.Add(account);
            }

            var customerAccount = new CustomerAccount
            {
                CustomerId = customerId,
                AccountNumber = accountNumber,
                OwnershipType = ownershipType,
                AccountStatus = "Active",
                OwnershipStartDate = DateTime.Now
            };

            _context.CustomerAccounts.Add(customerAccount);
            _context.SaveChanges();
        }

        public CustomerAccount? FindCustomerAccount(string accountNumber, int customerId)
        {
            return _context.CustomerAccounts
                .FirstOrDefault(ca => ca.AccountNumber == accountNumber && ca.CustomerId == customerId);
        }

        public void UpdateAccountStatus(CustomerAccount customerAccount, string newStatus)
        {
            customerAccount.AccountStatus = newStatus;
            _context.SaveChanges();
        }

        public bool RemoveAccountFromCustomer(string accountNumber, int customerId, out bool wasLastOwner)
        {
            wasLastOwner = false;

            var customerAccount = FindCustomerAccount(accountNumber, customerId);
            if (customerAccount == null)
                return false;

            _context.CustomerAccounts.Remove(customerAccount);
            _context.SaveChanges();

            bool hasOtherOwners = _context.CustomerAccounts
                .Any(ca => ca.AccountNumber == accountNumber);

            if (!hasOtherOwners)
            {
                var account = _context.Accounts.Find(accountNumber);
                if (account != null)
                {
                    _context.Accounts.Remove(account);
                    _context.SaveChanges();
                }
                wasLastOwner = true;
            }

            return true;
        }

        public List<Customer> GetAllCustomersWithAccounts()
        {
            return _context.Customers
                .Include(c => c.CustomerAccounts)
                    .ThenInclude(ca => ca.Account)
                        .ThenInclude(a => a.Branch)
                .ToList();
        }
    }
}
