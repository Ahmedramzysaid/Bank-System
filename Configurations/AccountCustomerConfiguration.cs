using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;    

namespace BankSystem.Configurations
{
    public class AccountCustomerConfiguration : IEntityTypeConfiguration<CustomerAccount>
    {
        
            

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.HasKey(ca => ca.Id);
            builder.HasOne(ca => ca.Customer)
                   .WithMany(c => c.CustomerAccounts)
                   .HasForeignKey(ca => ca.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ca => ca.Account)
                   .WithMany(a => a.CustomerAccounts)
                   .HasForeignKey(ca => ca.AccountNumber)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(ca => ca.AccountStatus)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(ca => ca.OwnershipType)
                    .HasMaxLength(50);
            builder
                    .HasIndex(ca => new { ca.CustomerId, ca.AccountNumber })
                    .IsUnique()
                    .HasFilter("[AccountNumber] IS NOT NULL");

               


        }
    }
    }
