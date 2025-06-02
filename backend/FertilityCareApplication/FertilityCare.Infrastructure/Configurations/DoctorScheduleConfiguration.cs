using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        builder.ToTable("DoctorSchedule");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn(1000, 1);

        builder.Property(ds => ds.WorkDate).HasColumnType("DATE");

        builder.Property(ds => ds.StartTime).HasColumnType("TIME");

        builder.Property(ds => ds.EndTime).HasColumnType("TIME");

        builder.Property(ds => ds.Note).HasColumnType("NTEXT");

        builder.Property(ds => ds.MaxAppointments).HasDefaultValue(10);

        builder.Property(ds => ds.CreatedAt).HasDefaultValueSql("GETDATE()");

        builder.HasOne(ds => ds.Doctor)
           .WithMany(d => d.DoctorSchedules)
           .HasForeignKey(ds => ds.DoctorId)
           .OnDelete(DeleteBehavior.Cascade)
           .HasConstraintName("FK_DoctorSchedule_Doctor");

    }
}
