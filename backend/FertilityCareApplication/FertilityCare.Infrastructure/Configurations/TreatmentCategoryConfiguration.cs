using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class TreatmentCategoryConfiguration : IEntityTypeConfiguration<TreatmentCategory>
{
    public void Configure(EntityTypeBuilder<TreatmentCategory> builder)
    {
        builder.ToTable("TreatmentCategory");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        builder.Property(x => x.Name).HasColumnType("NVARCHAR(255)");

        builder.Property(x => x.Description).HasColumnType("NTEXT");

        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

    }
}
