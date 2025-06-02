using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class TreatmentPaymentConfiguration : IEntityTypeConfiguration<TreatmentPayment>
{
    public void Configure(EntityTypeBuilder<TreatmentPayment> builder)
    {
        builder.ToTable("TreatmentPayment");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()");

        builder.Property(e => e.PaymentCode)
               .HasMaxLength(255)
               .IsRequired();

        builder.HasIndex(e => e.PaymentCode)
               .IsUnique();

        builder.Property(e => e.Amount)
               .HasColumnType("DECIMAL(18,2)")
               .IsRequired();

        builder.Property(e => e.TransactionCode)
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(e => e.Note)
               .HasColumnType("NTEXT");

        builder.Property(e => e.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

        builder.HasOne(e => e.UserProfile)
               .WithMany()
               .HasForeignKey(e => e.UserProfileId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_TreatmentPayment_UserProfile");

        builder.HasOne(e => e.PaymentMethod)
               .WithMany()
               .HasForeignKey(e => e.PaymentMethodId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_TreatmentPayment_PaymentMethod");

        builder.HasOne(e => e.TreatmentPlanStep)
               .WithMany()
               .HasForeignKey(e => e.TreatmentPlanStepId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_TreatmentPayment_TreatmentPlanStep");
    }
}
