using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class ServicePackagePlanStep
{

    public long Id { get; set; }

    public Guid ServicePackagePlanId { get; set; }

    public long? TreatmentStepId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public ServicePlanStepStatus Status { get; set; }

    public string? Note { get; set; }

    public bool? IsComplete { get; set; }   

    public DateTime? CompletedAt { get; set; }
}
