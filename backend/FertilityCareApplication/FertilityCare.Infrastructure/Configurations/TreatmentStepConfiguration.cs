using FertilityCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Configurations;

public class TreatmentStepConfiguration : IEntityTypeConfiguration<TreatmentStep>
{
    public void Configure(EntityTypeBuilder<TreatmentStep> builder)
    {
        builder.ToTable("TreatmentStep");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).UseIdentityColumn(1000,1);

        builder.Property(t => t.StepName).HasDefaultValue("NVARCHAR(255)").IsRequired();

        builder.Property(t => t.Description).HasColumnType("NTEXT");

        builder.Property(t => t.StepOrder).IsRequired();

        builder.HasOne(x => x.TreatmentService)
            .WithMany(t => t.TreatmentSteps)
            .HasForeignKey(t => t.TreatmentServiceId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_TreatmentStep_TreatmentService");
    }
}
