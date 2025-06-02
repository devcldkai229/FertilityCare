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

public class EmbryoDetailConfiguration : IEntityTypeConfiguration<EmbryoDetail>
{
    public void Configure(EntityTypeBuilder<EmbryoDetail> builder)
    {
        builder.ToTable("EmbryoDetail");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(e => e.Grade)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("NVARCHAR");

        builder.Property(e => e.IsViable)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.Note)
            .HasColumnType("NTEXT");

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UpdatedAt)
            .ValueGeneratedOnUpdate();

        builder.HasOne(e => e.EmbryoFertilization)
            .WithMany()
            .HasForeignKey(e => e.EmbryoFertilizationId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_EmbryoDetail_EmbryoFertilization");

        builder.HasOne(e => e.TreatmentPlan)
            .WithMany()
            .HasForeignKey(e => e.TreatmentPlanId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_EmbryoDetail_TreatmentPlan");
    }
}
