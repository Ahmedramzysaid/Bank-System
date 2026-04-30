using BankSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        { 
            builder
                   .HasKey(a => a.AccountNumber);
            builder
                   .Property(a => a.AccountNumber)
                   .HasMaxLength(50);
            builder
                   .Property(a => a.CurrentBalance)
                   .HasColumnType("decimal(18,2)");
            builder
                   .Property(a => a.AccountType)
                   .IsRequired()
                   .HasMaxLength(50);
            builder
                   .Property(a => a.OpeningDate)
                   .IsRequired();
            builder
                   .HasOne(a => a.Branch)
                   .WithMany(b => b.Accounts)
                   .HasForeignKey(a => a.BranchCode)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
