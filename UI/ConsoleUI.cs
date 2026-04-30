using BankSystem.Models;
using BankSystem.Services;

namespace BankSystem.UI
{
    public class ConsoleUI
    {
        private readonly BankService _service;

        public ConsoleUI(BankService service)
        {
            _service = service;
        }

        // ===== Main Menu Loop =====
        public async Task RunAsync()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("    National Bank - Management");
                Console.WriteLine("==========================================");
                Console.WriteLine("  1) Add a new Customer");
                Console.WriteLine("  2) Open a new Account for a Customer");
                Console.WriteLine("  3) Update Account Status (Active / Closed)");
                Console.WriteLine("  4) Remove an Account from a Customer");
                Console.WriteLine("  5) List all Customers (with accounts)");
                Console.WriteLine("  0) Exit");
                Console.WriteLine("------------------------------------------");
                Console.Write("  Enter choice: ");

                string choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        await AddCustomerScreenAsync();
                        break;
                    case "2":
                        OpenAccountScreen();
                        break;
                    case "3":
                        UpdateAccountStatusScreen();
                        break;
                    case "4":
                        RemoveAccountScreen();
                        break;
                    case "5":
                        ListCustomersScreen();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("\nGoodbye!");
                        continue;
                    default:
                        PrintError("Invalid choice. Please enter a number between 0 and 5.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }

        // ===== Screen 1: Add a new Customer =====
        private async Task AddCustomerScreenAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Add New Customer ---");

            Console.Write("Full Name    : ");
            string fullName = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(fullName))
            {
                PrintError("Full Name is required.");
                return;
            }

            Console.Write("National ID  : ");
            string nationalId = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(nationalId))
            {
                PrintError("National ID is required.");
                return;
            }

            Console.Write("Date of Birth : (yyyy-MM-dd) ");
            string dobInput = Console.ReadLine()?.Trim() ?? "";
            if (!DateTime.TryParse(dobInput, out DateTime dob))
            {
                PrintError("Invalid date format. Please use yyyy-MM-dd.");
                return;
            }

            Console.Write("Email        : ");
            string email = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                PrintError("A valid email is required.");
                return;
            }

            Console.Write("Phone        : ");
            string phone = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(phone))
            {
                PrintError("Phone number is required.");
                return;
            }

            Console.Write("Address      : ");
            string address = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(address))
            {
                PrintError("Address is required.");
                return;
            }

            Console.WriteLine("Customer Type:");
            Console.WriteLine("    1) Individual");
            Console.WriteLine("    2) Business");
            Console.Write(" Choice: ");
            string typeChoice = Console.ReadLine()?.Trim() ?? "";

            string customerType;
            switch (typeChoice)
            {
                case "1":
                    customerType = "Individual";
                    break;
                case "2":
                    customerType = "Business";
                    break;
                default:
                    PrintError("Invalid choice. Must be 1 or 2.");
                    return;
            }

            int customerId = await _service.AddCustomerAsync(fullName, nationalId, dob, email, phone, address, customerType);
            PrintSuccess($"Customer created successfully. CustomerId = {customerId}");
        }

        // ===== Screen 2: Open a new Account =====
        private void OpenAccountScreen()
        {
            Console.Clear();
            Console.WriteLine("--- Open New Account ---");

            Console.Write("Account Number : ");
            string accountNumber = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                PrintError("Account Number is required.");
                return;
            }

            Console.WriteLine("Account Type:");
            Console.WriteLine("    1) Savings");
            Console.WriteLine("    2) Current");
            Console.WriteLine("    3) Business");
            Console.Write(" Choice: ");
            string typeChoice = Console.ReadLine()?.Trim() ?? "";

            string accountType;
            switch (typeChoice)
            {
                case "1":
                    accountType = "Savings";
                    break;
                case "2":
                    accountType = "Current";
                    break;
                case "3":
                    accountType = "Business";
                    break;
                default:
                    PrintError("Invalid choice. Must be 1, 2, or 3.");
                    return;
            }

            Console.Write("Branch Code   : ");
            string branchInput = Console.ReadLine()?.Trim() ?? "";
            if (!int.TryParse(branchInput, out int branchCode))
            {
                PrintError("Invalid Branch Code. Must be a number.");
                return;
            }

            Console.Write("Customer Id   : ");
            string customerInput = Console.ReadLine()?.Trim() ?? "";
            if (!int.TryParse(customerInput, out int customerId))
            {
                PrintError("Invalid Customer ID. Must be a number.");
                return;
            }

            Console.WriteLine("Ownership Role:");
            Console.WriteLine("    1) Primary");
            Console.WriteLine("    2) CoHolder");
            Console.Write(" Choice: ");
            string roleChoice = Console.ReadLine()?.Trim() ?? "";

            string ownershipType;
            switch (roleChoice)
            {
                case "1":
                    ownershipType = "Primary";
                    break;
                case "2":
                    ownershipType = "CoHolder";
                    break;
                default:
                    PrintError("Invalid choice. Must be 1 or 2.");
                    return;
            }

            // Validate
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Validating branch '{branchCode}' and customer #{customerId}...");
            Console.ResetColor();

            if (!_service.BranchExists(branchCode))
            {
                PrintError($"Branch with Code '{branchCode}' does not exist.");
                return;
            }

            if (!_service.CustomerExists(customerId))
            {
                PrintError($"Customer with ID '{customerId}' does not exist.");
                return;
            }

            _service.OpenAccount(accountNumber, accountType, branchCode, customerId, ownershipType);
            PrintSuccess($"Account '{accountNumber}' created and linked to customer {customerId} as {ownershipType} owner.");
        }

        // ===== Screen 3: Update Account Status =====
        private void UpdateAccountStatusScreen()
        {
            Console.Clear();
            Console.WriteLine("--- Update Account Status ---");

            Console.Write("Account Number : ");
            string accountNumber = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                PrintError("Account Number is required.");
                return;
            }

            Console.Write("Customer Id    : ");
            string customerInput = Console.ReadLine()?.Trim() ?? "";
            if (!int.TryParse(customerInput, out int customerId))
            {
                PrintError("Invalid Customer ID. Must be a number.");
                return;
            }

            var customerAccount = _service.FindCustomerAccount(accountNumber, customerId);
            if (customerAccount == null)
            {
                PrintError($"No account found with Account Number '{accountNumber}' for Customer ID '{customerId}'.");
                return;
            }

            Console.WriteLine("New Status:");
            Console.WriteLine("    1) Active");
            Console.WriteLine("    2) Closed");
            Console.Write(" Choice: ");
            string statusChoice = Console.ReadLine()?.Trim() ?? "";

            string newStatus;
            switch (statusChoice)
            {
                case "1":
                    newStatus = "Active";
                    break;
                case "2":
                    newStatus = "Closed";
                    break;
                default:
                    PrintError("Invalid choice. Must be 1 or 2.");
                    return;
            }

            _service.UpdateAccountStatus(customerAccount, newStatus);
            PrintSuccess($"Status updated to {newStatus}.");
        }

        // ===== Screen 4: Remove Account from Customer =====
        private void RemoveAccountScreen()
        {
            Console.Clear();
            Console.WriteLine("--- Remove Account From Customer ---");

            Console.Write("Account Number : ");
            string accountNumber = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                PrintError("Account Number is required.");
                return;
            }

            Console.Write("Customer Id    : ");
            string customerInput = Console.ReadLine()?.Trim() ?? "";
            if (!int.TryParse(customerInput, out int customerId))
            {
                PrintError("Invalid Customer ID. Must be a number.");
                return;
            }

            bool removed = _service.RemoveAccountFromCustomer(accountNumber, customerId, out bool wasLastOwner);

            if (!removed)
            {
                PrintError($"No link found between Account '{accountNumber}' and Customer ID '{customerId}'.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n  Ownership link deleted.");
            if (wasLastOwner)
            {
                Console.WriteLine($"     That was the last owner - account '{accountNumber}' was also removed.");
            }
            Console.ResetColor();
        }

        // ===== Screen 5: List all Customers =====
        private void ListCustomersScreen()
        {
            Console.Clear();
            Console.WriteLine("--- All Customers ---\n");

            var customers = _service.GetAllCustomersWithAccounts();

            if (!customers.Any())
            {
                Console.WriteLine("  No customers found.");
                return;
            }

            foreach (var customer in customers)
            {
                Console.Write($"  #{customer.Id} {customer.FullName} ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"({customer.CustomerType})");
                Console.ResetColor();

                if (customer.CustomerAccounts.Any())
                {
                    foreach (var ca in customer.CustomerAccounts)
                    {
                        string branchName = ca.Account?.Branch?.Name ?? "N/A";
                        string accType = ca.Account?.AccountType ?? "N/A";
                        decimal balance = ca.Account?.CurrentBalance ?? 0;

                        Console.Write($"        {ca.AccountNumber}  {accType}  Balance: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{balance:N2}");
                        Console.ResetColor();
                        Console.Write($"  {ca.OwnershipType}  ");

                        if (ca.AccountStatus == "Active")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Active");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(ca.AccountStatus);
                        }
                        Console.ResetColor();

                        Console.WriteLine($"  @ {branchName}");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("        (no accounts)");
                    Console.ResetColor();
                }
            }
        }

        // ===== Helpers =====
        private void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
        }

        private void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  {message}");
            Console.ResetColor();
        }
    }
}
