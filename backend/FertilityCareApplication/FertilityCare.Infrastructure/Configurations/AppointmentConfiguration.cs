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

        builder.Property(x => x.AppointmentDate).IsRequired();

        builder.Property(x => x.BookingEmail).HasColumnType("NVARCHAR(MAX)");

        builder.Property(x => x.BookingPhone).HasColumnType("NVARCHAR(12)");

        builder.Property(x => x.StartTime).HasColumnType("TIME");

        builder.Property(x => x.EndTime).HasColumnType("TIME");

        builder.Property(x => x.Note).HasColumnType("NTEXT");

        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

        builder.HasOne<Patient>()
            .WithMany()
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Appointment_Patient");

        builder.HasOne<Doctor>()
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Appointment_Doctor");

        builder.HasOne<DoctorSchedule>()
            .WithMany()
            .HasForeignKey(x => x.DoctorScheduleId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Appointment_DoctorSchedule");

        builder.HasOne<TreatmentService>()
            .WithMany()
            .HasForeignKey(x => x.TreatmentServiceId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Appointment_TreatmentService");

    }
}
