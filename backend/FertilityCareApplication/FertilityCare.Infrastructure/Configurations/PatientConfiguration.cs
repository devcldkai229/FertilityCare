using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patient");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");

        builder.Property(p => p.MedicalHistory).HasColumnType("NTEXT");

        builder.Property(p => p.FertilityDiagnosis).HasColumnType("NVARCHAR(MAX)");

        builder.Property(p => p.AllergiesNotes).HasColumnType("NTEXT");

        builder.Property(p => p.BloodType).HasColumnType("NVARCHAR(20)");

        builder.Property(p => p.Height).HasColumnType("DECIMAL(5,2)");

        builder.Property(p => p.Weight).HasColumnType("DECIMAL(5,2)");

        builder.Property(p => p.MaritalStatus).HasColumnType("NVARCHAR(50)");

        builder.Property(p => p.Note).HasColumnType("NTEXT");

        builder.HasOne<PatientPartner>()
            .WithOne()
            .HasForeignKey<Patient>(p => p.PatientParnerId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Patient_Partner");


        builder.HasOne(p => p.UserProfile)
            .WithOne()
            .HasForeignKey<Patient>(p => p.UserProfileId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Patient_UserProfile");

        builder.HasIndex(p => p.UserProfileId).IsUnique();

    }
}
