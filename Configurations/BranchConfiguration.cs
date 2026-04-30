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
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
              builder.HasKey(b => b.Code);
              builder.Property(b => b.Name)
                     .IsRequired()
                     .HasMaxLength(100);
              builder.Property(b => b.Address)
                     .IsRequired()
                     .HasMaxLength(200);
              builder.Property(b => b.PhoneNumber)
                     .IsRequired()
                     .HasMaxLength(15);
        }
    }
}
