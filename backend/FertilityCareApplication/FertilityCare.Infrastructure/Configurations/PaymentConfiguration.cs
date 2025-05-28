using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasDefaultValueSql("NEWID()");

        builder.Property(e => e.PaymentCode).HasMaxLength(255);

        builder.HasIndex(e => e.PaymentCode).IsUnique();

        builder.Property(e => e.Amount).HasColumnType("DECIMAL(18,2)").IsRequired();

        builder.Property(e => e.Status).HasMaxLength(100).IsRequired();

        builder.Property(e => e.RefundAmount).HasColumnType("DECIMAL(18,2)");

        builder.Property(e => e.RefundReason).HasColumnType("NVARCHAR(MAX)");

        builder.Property(e => e.Notes).HasColumnType("NTEXT");

        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

        builder.HasOne(e => e.UserProfile)
            .WithMany()
            .HasForeignKey(e => e.UserProfileId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Payment_UserProfile");

        builder.HasOne(e => e.PaymentMethod)
            .WithMany()
            .HasForeignKey(e => e.PaymentMethodId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Payment_PaymentMethod");

        builder.HasOne(e => e.ServicePackagePlan)
            .WithMany()
            .HasForeignKey(e => e.ServicePackagePlanId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Payment_ServicePackagePlan"); 

    }
}
