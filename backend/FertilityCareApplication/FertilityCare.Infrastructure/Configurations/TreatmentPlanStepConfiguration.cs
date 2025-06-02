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

public class TreatmentPlanStepConfiguration : IEntityTypeConfiguration<TreatmentPlanStep>
{
    public void Configure(EntityTypeBuilder<TreatmentPlanStep> builder)
    {
        builder.ToTable("TreatmentPlanStep");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn(1000, 1);

        builder.Property(x => x.StartDate)
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v))
            .HasColumnType("DATE")
            .IsRequired();

        builder.Property(x => x.EndDate)
            .HasConversion(
                v => v.HasValue ? v.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                v => v.HasValue ? DateOnly.FromDateTime(v.Value) : (DateOnly?)null)
            .HasColumnType("DATE");

        builder.Property(x => x.Note)
            .HasColumnType("NTEXT");

        builder.Property(x => x.IsCompleted)
            .HasDefaultValue(false);

        builder.Property(x => x.StepPrice)
            .HasColumnType("DECIMAL(18,2)");

        builder.Property(x => x.Status)
            .HasDefaultValue(TreatmentPlanStepStatus.Planned);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("DATETIME");

        builder.HasOne(x => x.TreatmentPlan)
            .WithMany(tp => tp.TreatmentPlanSteps!)
            .HasForeignKey(x => x.TreatmentPlanId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_TreatmentPlanStep_TreatmentPlan");

        builder.HasOne<TreatmentStep>()
            .WithMany()
            .HasForeignKey(x => x.TreatmentStepId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_TreatmentPlanStep_TreatmentStep");
    }
}