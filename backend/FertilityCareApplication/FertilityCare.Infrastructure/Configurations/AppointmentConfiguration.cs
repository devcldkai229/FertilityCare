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

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointment");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AppointmentDate)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired()
            .HasColumnType("TIME");

        builder.Property(x => x.EndTime)
            .IsRequired()
            .HasColumnType("TIME");

        builder.Property(x => x.BookingEmail)
            .HasMaxLength(256)
            .HasColumnType("NVARCHAR");

        builder.Property(x => x.BookingPhone)
            .HasMaxLength(12)
            .HasColumnType("NVARCHAR");

        builder.Property(x => x.Note)
            .HasColumnType("NTEXT");

        builder.Property(x => x.CancellationReason)
            .HasMaxLength(512)
            .HasColumnType("NVARCHAR");

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedOnUpdate();

        builder.HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Appointment_Patient");

        builder.HasOne(x => x.Doctor)
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Appointment_Doctor");

        builder.HasOne(x => x.DoctorSchedule)
            .WithMany()
            .HasForeignKey(x => x.DoctorScheduleId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Appointment_DoctorSchedule");

        builder.HasOne(x => x.TreatmentService)
            .WithMany()
            .HasForeignKey(x => x.TreatmentServiceId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Appointment_TreatmentService");
    }
}
