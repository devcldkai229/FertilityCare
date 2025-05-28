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
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TransferDate).HasColumnType("DATETIME");

            builder.Property(x => x.CreatedAt).HasColumnType("DATETIME");

            builder.Property(x => x.UpdatedAt).HasColumnType("DATETIME");

            builder.Property(x => x.PregnancyResultNote).HasColumnType("NTEXT");

            builder.Property(x => x.Note).HasColumnType("NTEXT");

            builder.Property(x => x.FeeCharged).HasColumnType("DECIMAL(10,2)");

            builder.HasOne(x => x.EmbryoDetail)
                .WithMany()
                .HasForeignKey(x => x.EmbryoDetailId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_EmbryoTransfer_EmbryoDetail");

            builder.HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_EmbryoTransfer_Doctor");

        }
    }
}
