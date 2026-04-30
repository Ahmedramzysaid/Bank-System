using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankSystem.Models;

namespace BankSystem.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
              optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS04;Database=BankSystemDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            
            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    Code = 1,
                    Name = "Cairo Downtown Branch",
                    Address = "15 Tahrir Square, Downtown, Cairo",
                    PhoneNumber = "0221234567"
                },
                new Branch
                {
                    Code = 2,
                    Name = "Alexandria Main Branch",
                    Address = "42 Corniche Road, Alexandria",
                    PhoneNumber = "0339876543"
                }
            );

          
            modelBuilder.Entity<Manager>().HasData(
                new Manager
                {
                    Id = 1,
                    FullName = "Ahmed Hassan",
                    Email = "ahmed.hassan@nationalbank.eg",
                    PhoneNumber = "01012345678",
                    HireDate = new DateTime(2020, 3, 15),
                    BranchCode = 1
                },
                new Manager
                {
                    Id = 2,
                    FullName = "Sara Mohamed",
                    Email = "sara.mohamed@nationalbank.eg",
                    PhoneNumber = "01098765432",
                    HireDate = new DateTime(2021, 7, 1),
                    BranchCode = 2
                }
            );
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
         public DbSet<CustomerAccount> CustomerAccounts { get; set; }
         public DbSet<Transcation> Transcations { get; set; }
    }
}
