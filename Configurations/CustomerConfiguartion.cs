using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BankSystem.Configurations
{
    public class CustomerConfiguartion : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
             builder.HasKey(c => c.Id);
             builder.Property(c => c.FullName).
                    IsRequired()
                    .HasMaxLength(100);
             builder
                   .Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(100);
             builder
                   .Property(c => c.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(15);
             builder
                   .Property(c => c.DateOfBirth)
                   .IsRequired();
             builder
                   .Property(c => c.Address)
                   .IsRequired()
                   .HasMaxLength(200);
             builder
                   .Property(c => c.NationalId)
                   .IsRequired()
                   .HasMaxLength(20);
             builder
                   .Property(c => c.CustomerType)
                   .IsRequired()
                   .HasMaxLength(50);

             builder.HasMany(c => c.CustomerAccounts)
                    .WithOne(ca => ca.Customer)
                    .HasForeignKey(ca => ca.CustomerId);
        }
    }
}
