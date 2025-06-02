using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class TreatmentPlanStep
{
    public long Id { get; set; }

    public Guid TreatmentPlanId { get; set; }

    public virtual TreatmentPlan TreatmentPlan { get; set; } = null!;

    public long TreatmentStepId { get; set; }

    public string Note { get; set; } = "";

    public decimal StepPrice { get; set; }

    public TreatmentPlanStepStatus Status { get; set; } = TreatmentPlanStepStatus.Planned;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

}
