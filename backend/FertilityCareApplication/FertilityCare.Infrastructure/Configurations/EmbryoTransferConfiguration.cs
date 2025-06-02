using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations
{
    public class EmbryoTransferConfiguration : IEntityTypeConfiguration<EmbryoTransfer>
    {
        public void Configure(EntityTypeBuilder<EmbryoTransfer> builder)
        {
            builder.ToTable("EmbryoTransfer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TransferDate)
                .IsRequired()
                .HasColumnType("DATETIME");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("DATETIME");

            builder.Property(x => x.PregnancyResultNote)
                .HasColumnType("NTEXT");

            builder.Property(x => x.Note)
                .HasColumnType("NTEXT");

            builder.Property(x => x.FeeCharged)
                .HasColumnType("DECIMAL(10,2)")
                .IsRequired();

            builder.Property(x => x.IsFrozenTransfer)
                .HasDefaultValue(false);

            builder.Property(x => x.IsSuccessful)
                .HasDefaultValue(false);

            builder.HasOne(x => x.EmbryoDetail)
                .WithMany()
                .HasForeignKey(x => x.EmbryoDetailId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_EmbryoTransfer_EmbryoDetail");

            builder.HasOne(x => x.TreatmentPlan)
                .WithMany()
                .HasForeignKey(x => x.TreatmentPlanId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_EmbryoTransfer_TreatmentPlan");

            builder.HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_EmbryoTransfer_Doctor");
        }
    }
}
