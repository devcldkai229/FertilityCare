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

public class ServicePackagePlanConfiguration : IEntityTypeConfiguration<ServicePackagePlan>
{
    public void Configure(EntityTypeBuilder<ServicePackagePlan> builder)
    {
        builder.ToTable("ServicePackagePlan");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        builder.Property(x => x.StartDate).HasColumnType("DATE").HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.EndDate).HasColumnType("DATE");

        builder.Property(x => x.TotalCost).HasColumnType("DECIMAL(18,2)");

        builder.Property(x => x.Note).HasColumnType("NTEXT");

        builder.Property(x => x.CreatedAt).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");

        builder.HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ServicePackagePlan_Patient");

        builder.HasOne(x => x.Doctor)
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ServicePackagePlan_Doctor");

        builder.HasOne(x => x.TreatmentService)
            .WithMany()
            .HasForeignKey(x => x.TreatmentServiceId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ServicePackagePlan_TreatmentService");
    }
}
