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

public class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
{
    public void Configure(EntityTypeBuilder<TestResult> builder)
    {
        builder.ToTable("TestResult");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn(1000, 1);

        builder.Property(x => x.TestName).IsRequired();

        builder.Property(x => x.Note).HasColumnType("NTEXT");

        builder.Property(x => x.TestDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");

        builder.HasOne(x => x.TreatmentPlan)
            .WithMany()
            .HasForeignKey(x => x.TreatmentPlanId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_TestResult_ServicePackagePlan");


    }
}
