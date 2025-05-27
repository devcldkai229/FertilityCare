using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class EggRetrievalCycleConfiguration : IEntityTypeConfiguration<EggRetrievalCycle>
{
    public void Configure(EntityTypeBuilder<EggRetrievalCycle> builder)
    {
        builder.ToTable("EggRetrievalCycle");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasDefaultValueSql("NEWID()");

        builder.Property(e => e.CycleNumber).IsRequired();

        builder.Property(e => e.MatureEggs).HasDefaultValue(0);

        builder.Property(e => e.ImmatureEggs).HasDefaultValue(0);

        builder.Property(e => e.AbnormalEggs).HasDefaultValue(0);

        builder.HasOne(e => e.ServicePackagePlan)
               .WithMany()
               .HasForeignKey(e => e.ServicePackagePlanId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_EggRetrievalCycle_ServicePackagePlan");

        builder.HasOne(e => e.Doctor)
               .WithMany()
               .HasForeignKey(e => e.DoctorId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_EggRetrievalCycle_Doctor");
    }
}