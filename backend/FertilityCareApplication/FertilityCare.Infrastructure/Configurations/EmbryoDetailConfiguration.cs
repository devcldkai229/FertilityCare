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

        builder.Property(e => e.Id).HasDefaultValueSql("NEWID()");

        builder.Property(e => e.Grade).HasMaxLength(20).IsRequired();

        builder.Property(e => e.IsViable).HasDefaultValue(true);

        builder.Property(e => e.Status)
               .HasConversion<int>()
               .HasColumnType("INT")
               .HasDefaultValue((int)EmbryoStatus.Available);

        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

        builder.HasOne<EmbryoFertilization>()
               .WithMany()
               .HasForeignKey(e => e.EmbryoFertilizationId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_EmbryoDetail_EmbryoFertilization");
    }
}