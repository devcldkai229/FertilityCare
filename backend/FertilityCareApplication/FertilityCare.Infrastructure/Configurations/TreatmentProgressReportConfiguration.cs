using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class TreatmentProgressReportConfiguration : IEntityTypeConfiguration<TreatmentProgressReport>
{
    public void Configure(EntityTypeBuilder<TreatmentProgressReport> builder)
    {
        builder.ToTable("TreatmentProgressReport");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        builder.Property(x => x.ReportDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.CreatedAt).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");
        
        builder.Property(x => x.NextSteps).HasColumnType("NTEXT").IsRequired();

        builder.HasOne<ServicePackagePlan>()
            .WithMany()
            .HasForeignKey(x => x.ServicePackagePlanId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_TreatmentProgressReport_ServicePackagePlan");

        builder.HasOne<Doctor>()
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_TreatmentProgressReport_Doctor");
    }
}
