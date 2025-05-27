using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class ServicePackagePlanExtensionConfiguration : IEntityTypeConfiguration<ServicePackagePlanExtension>
{
    public void Configure(EntityTypeBuilder<ServicePackagePlanExtension> builder)
    {
        builder.ToTable("ServicePackagePlanExtension");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn(1000, 1);

        builder.Property(x => x.StepName).IsRequired();

        builder.Property(x => x.StartDate).HasColumnType("DATETIME");

        builder.Property(x => x.EndDate).HasColumnType("DATETIME");

        builder.Property(x => x.CompletedAt).HasColumnType("DATETIME");

        builder.Property(x => x.Note).HasColumnType("NTEXT");

        builder.Property(x => x.ExtraFee).HasColumnType("DECIMAL(18,2)");

        builder.Property(x => x.IsComplete).HasDefaultValue(false);

        builder.HasOne<ServicePackagePlan>()
            .WithMany()
            .HasForeignKey(x => x.ServicePackagePlanId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ServicePackagePlanExtension_ServicePackagePlan");
    }

}
