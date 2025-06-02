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

public class FrozenEmbryoStorageConfiguration : IEntityTypeConfiguration<FrozenEmbryoStorage>
{
    public void Configure(EntityTypeBuilder<FrozenEmbryoStorage> builder)
    {
        builder.ToTable("FrozenEmbryoStorage");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()");

        builder.Property(e => e.StorageTank)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(e => e.StorageStartDate)
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(e => e.StorageEndDate)
               .HasColumnType("datetime");

        builder.Property(e => e.FreezeMethod)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.MonthlyStorageFee)
               .HasColumnType("decimal(10,2)");

        builder.Property(e => e.Status)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.SurvivalAfterThaw)
               .HasDefaultValue(true);

        builder.Property(e => e.Note)
               .HasColumnType("nvarchar(max)");

        builder.Property(e => e.CreatedAt)
               .HasColumnType("datetime")
               .HasDefaultValueSql("GETDATE()");

        builder.Property(e => e.UpdatedAt)
               .HasColumnType("datetime");

        builder.HasOne(e => e.EmbryoDetail)
               .WithMany()
               .HasForeignKey(e => e.EmbryoDetailId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_FrozenEmbryoStorage_EmbryoDetail");

        builder.HasOne(e => e.TreatmentPlan)
               .WithMany()
               .HasForeignKey(e => e.TreatmentPlanId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_FrozenEmbryoStorage_TreatmentPlan");
    }
}
