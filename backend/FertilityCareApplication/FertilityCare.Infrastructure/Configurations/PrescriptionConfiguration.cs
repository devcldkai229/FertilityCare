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

internal class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescription");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.PrescriptionDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.Note).HasColumnType("NTEXT");

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .HasDefaultValue((int)PrescriptionStatus.Active);

        builder.HasOne<ServicePackagePlan>()
            .WithMany()
            .HasForeignKey(x => x.ServicePackagePlanId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Prescription_ServicePackagePlan");
    }
}
