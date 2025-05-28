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

public class MonitorReminderConfiguration : IEntityTypeConfiguration<MonitorReminder>
{
    public void Configure(EntityTypeBuilder<MonitorReminder> builder)
    {
        builder.ToTable("MonitorReminder");

        builder.HasKey(mr => mr.Id);

        builder.Property(mr => mr.Id).UseIdentityColumn(1000, 1);

        builder.Property(mr => mr.Title).HasColumnType("NVARCHAR(255)").IsRequired(false);

        builder.Property(mr => mr.Description).HasColumnType("NTEXT");

        builder.Property(mr => mr.ReminderDate).HasColumnType("DATETIME").IsRequired();

        builder.Property(mr => mr.IsComplete).HasColumnType("BIT").HasDefaultValue(false);

        builder.Property(mr => mr.CreatedAt).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");

        builder.HasOne<Patient>()
            .WithMany()
            .HasForeignKey(mr => mr.PatientId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_MonitorReminder_Patient");

        builder.HasOne<ServicePackagePlan>()
            .WithMany()
            .HasForeignKey(mr => mr.ServicePackagePlanId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_MonitorReminder_ServicePackagePlan");

        builder.HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey(mr => mr.SenderId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_MonitorReminder_Sender");

    }
}
