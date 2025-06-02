using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class MediaFilesConfiguration : IEntityTypeConfiguration<MediaFile>
{
    public void Configure(EntityTypeBuilder<MediaFile> builder)
    {
        builder.ToTable("MediaFile");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id).HasDefaultValueSql("NEWID()");

        builder.Property(m => m.PublicId).HasColumnType("nvarchar(max)");

        builder.Property(m => m.Url).HasColumnType("nvarchar(max)");

        builder.Property(m => m.SecureUrl).HasColumnType("nvarchar(max)");

        builder.Property(m => m.Folder).HasMaxLength(255);

        builder.Property(m => m.FileName).HasMaxLength(255);

        builder.Property(m => m.FileType).HasMaxLength(100);

        builder.Property(m => m.ResourceType).HasMaxLength(50);

        builder.Property(m => m.Format).HasMaxLength(50);

        builder.Property(m => m.Size);

        builder.Property(m => m.Width);

        builder.Property(m => m.Height);

        builder.Property(m => m.Duration).HasColumnType("decimal(10,2)");

        builder.Property(m => m.Tags).HasColumnType("nvarchar(max)");

        builder.Property(m => m.UploadedAt).HasDefaultValueSql("GETDATE()");

        builder.HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey(m => m.OwnerId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_MediaFile_UserProfile");
    }
}
