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

public class AppointmentReminderConfiguration : IEntityTypeConfiguration<AppointmentReminder>
{
    public void Configure(EntityTypeBuilder<AppointmentReminder> builder)
    {
        builder.ToTable("AppointmentReminder");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn(1000, 1);

        builder.Property(x => x.AppointmentId).IsRequired();

        builder.Property(x => x.PatientId).IsRequired();

        builder.Property(x => x.ReminderDate).HasColumnType("DATETIME");

        builder.Property(x => x.ReminderMethod).HasColumnType("NVARCHAR(100)");

        builder.Property(x => x.IsSent).HasDefaultValue(false);

        builder.Property(x => x.SentAt).HasColumnType("DATETIME");

        builder.Property(x => x.Note).HasColumnType("NTEXT");

        builder.Property(x => x.CreatedAt).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");

        builder.HasOne(x => x.Appointment)
            .WithMany()
            .HasForeignKey(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_AppointmentReminder_Appointment");

        builder.HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_AppointmentReminder_Patient");
    }
}
