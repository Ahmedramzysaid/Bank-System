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
    public class TranscationConfiguration :  IEntityTypeConfiguration<Transcation>
    {
        public void Configure(EntityTypeBuilder<Transcation> builder)
        {
            builder.HasKey(t => t.TranscationNumber);
            builder.Property(t => t.TranscationNumber)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(t => t.TranscationType)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(t => t.Note)
                   .HasMaxLength(200);
            builder.Property(t => t.Amount)
                   .HasColumnType("decimal(18,2)");
            builder.HasOne(t => t.Account)
                   .WithMany(a => a.Transactions)
                   .HasForeignKey(t => t.AccountNumber);
        }
    }
}