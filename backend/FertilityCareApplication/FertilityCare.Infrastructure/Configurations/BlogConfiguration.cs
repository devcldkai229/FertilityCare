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

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("Blog");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).UseIdentityColumn(1000, 1);

        builder.Property(b => b.UserProfileId).IsRequired();

        builder.HasOne(b => b.UserProfile)
               .WithMany()
               .HasForeignKey(b => b.UserProfileId)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_Blog_UserProfile");

        builder.Property(b => b.Title).IsRequired().HasColumnType("nvarchar(max)");

        builder.Property(b => b.Summary).HasColumnType("nvarchar(max)");

        builder.Property(b => b.Content).HasColumnType("ntext").IsRequired();

        builder.Property(b => b.FeaturedImageUrl).HasColumnType("nvarchar(max)");

        builder.Property(b => b.MetaKeywords).HasColumnType("nvarchar(max)");

        builder.Property(b => b.MetaDescription).HasColumnType("nvarchar(max)");

    }
}
