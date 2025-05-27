using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FertilityCare.Infrastructure.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctor");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id).HasDefaultValueSql("NEWID()");

        builder.HasOne(d => d.UserProfile)
            .WithOne()
            .HasForeignKey<Doctor>(d => d.UserProfileId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Doctor_UserProfile");

        builder.HasIndex(d => d.UserProfileId).IsUnique();

        builder.Property(d => d.UserProfileId).IsRequired();

        builder.Property(d => d.Rating).HasColumnType("DECIMAL(3,2)");

        builder.Property(d => d.IsAcceptingPatients).HasDefaultValue(true);

        builder.Property(d => d.PatientsServed).HasDefaultValue(0);
    }
}
                      