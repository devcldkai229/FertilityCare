using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class TreatmentStep
{

    public long Id { get; set; }

    public Guid TreatmentServiceId { get; set; }

    public string StepName { get; set; } = null!;

    public string? Description { get; set; }

    public int StepOrder { get; set; }

    public int? EstimatedDurationDays { get; set; }

    public bool IsOptional { get; set; }

    public bool IsActive { get; set; }

}
