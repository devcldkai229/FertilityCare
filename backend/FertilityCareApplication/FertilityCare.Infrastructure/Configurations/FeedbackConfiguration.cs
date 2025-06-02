using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("Feedback");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
               .HasDefaultValueSql("NEWID()");

        builder.Property(f => f.Rating)
               .HasColumnType("decimal(3,1)")
               .IsRequired();

        builder.Property(f => f.TreatmentQualityRating)
               .HasColumnType("decimal(3,1)")
               .IsRequired();

        builder.Property(f => f.Comment)
               .HasColumnType("nvarchar(max)");

        builder.Property(f => f.IsDisplayed)
               .HasDefaultValue(false);

        builder.Property(f => f.CreatedAt)
               .HasColumnType("datetime")
               .HasDefaultValueSql("GETDATE()");

        builder.Property(f => f.UpdatedAt)
               .HasColumnType("datetime");

        builder.HasOne(f => f.Patient)
               .WithMany()
               .HasForeignKey(f => f.PatientId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_Feedback_Patient");

        builder.HasOne(f => f.Doctor)
               .WithMany()
               .HasForeignKey(f => f.DoctorId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_Feedback_Doctor");

        builder.HasOne(f => f.TreatmentPlan)
               .WithMany()
               .HasForeignKey(f => f.TreatmentPlanId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_Feedback_TreatmentPlan");
    }
}
