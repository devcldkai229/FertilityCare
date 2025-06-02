using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class EmbryoFertilizationConfiguration : IEntityTypeConfiguration<EmbryoFertilization>
{
    public void Configure(EntityTypeBuilder<EmbryoFertilization> builder)
    {
        builder.ToTable("EmbryoFertilization");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(e => e.FertilizationDate)
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v))
            .HasColumnType("date")
            .IsRequired();

        builder.Property(e => e.FertilizationMethod)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("NVARCHAR");

        builder.Property(e => e.TotalEggsUsed)
            .IsRequired();

        builder.Property(e => e.TotalEggsFertilized)
            .HasDefaultValue(0);

        builder.Property(e => e.TotalEmbryosFormed)
            .HasDefaultValue(0);

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UpdatedAt)
            .ValueGeneratedOnUpdate();

        builder.HasOne(e => e.EggRetrievalCycle)
            .WithMany()
            .HasForeignKey(e => e.EggRetrievalCycleId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_EmbryoFertilization_EggRetrievalCycle");

        builder.HasOne(e => e.TreatmentPlan)
            .WithMany()
            .HasForeignKey(e => e.TreatmentPlanId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_EmbryoFertilization_TreatmentPlan");
    }
}
