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

        builder.Property(e => e.Id).HasDefaultValueSql("NEWID()");

        builder.Property(e => e.StorageTank).HasMaxLength(50).IsRequired();

        builder.Property(e => e.FreezeMethod)
               .HasConversion<int>()
               .HasColumnType("INT");

        builder.Property(e => e.Status)
               .HasConversion<int>()
               .HasColumnType("INT")
               .HasDefaultValue((int)FrozenEmbryoStorageStatus.Active);

        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

        builder.HasOne(e => e.EmbryoDetail)
               .WithMany()
               .HasForeignKey(e => e.EmbryoDetailId)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_FrozenEmbryoStorage_EmbryoDetail");
    }
}
