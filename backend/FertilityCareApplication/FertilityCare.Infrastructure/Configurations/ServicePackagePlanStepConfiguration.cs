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

public class ServicePackagePlanStepConfiguration : IEntityTypeConfiguration<ServicePackagePlanStep>
{
    public void Configure(EntityTypeBuilder<ServicePackagePlanStep> builder)
    {
        builder.ToTable("ServicePackagePlanStep");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn(1000, 1);

        builder.Property(x => x.StartDate).HasColumnType("DATETIME");

        builder.Property(x => x.EndDate).HasColumnType("DATETIME");

        builder.Property(x => x.CompletedAt).HasColumnType("DATETIME");

        builder.Property(x => x.Note).HasColumnType("NTEXT");

        builder.Property(x => x.IsComplete).HasDefaultValue(false);

        builder.HasOne<TreatmentStep>()
            .WithMany()
            .HasForeignKey(x => x.TreatmentStepId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ServicePackagePlanStep_TreatmentStep");

        builder.HasOne<ServicePackagePlan>()
            .WithMany(x => x.ServicePackagePlanSteps)
            .HasForeignKey(x => x.ServicePackagePlanId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ServicePackagePlanStep_ServicePackagePlan");

    }
}
