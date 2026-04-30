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
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
                builder.HasKey(m => m.Id);
                builder.Property(m => m.FullName)
                       .IsRequired()
                       .HasMaxLength(100);
                builder.Property(m => m.Email)
                       .IsRequired()
                       .HasMaxLength(100);
                builder.Property(m => m.PhoneNumber)
                       .IsRequired()
                       .HasMaxLength(15);
                builder.HasOne(m => m.Branch)
                    .WithOne(b => b.Manager)
                    .HasForeignKey<Manager>(m => m.BranchCode);
        }
    }
}
