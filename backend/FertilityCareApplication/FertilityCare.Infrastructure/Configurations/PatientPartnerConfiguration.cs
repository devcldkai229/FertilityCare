using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class PatientPartnerConfiguration : IEntityTypeConfiguration<PatientPartner>
{
    public void Configure(EntityTypeBuilder<PatientPartner> builder)
    {
        builder.ToTable("PatientPartner");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");

        builder.Property(p => p.FullName).HasColumnType("NVARCHAR(500)").IsRequired();

        builder.Property(p => p.Email).HasColumnType("NVARCHAR(255)");

        builder.Property(p => p.CreatedAt).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.UpdatedAt).HasColumnType("DATETIME");
    }
}
