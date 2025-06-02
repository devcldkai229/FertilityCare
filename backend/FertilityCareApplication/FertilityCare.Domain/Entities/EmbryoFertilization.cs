using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class EmbryoFertilization
{
    public Guid Id { get; set; }

    public Guid EggRetrievalCycleId { get; set; }

    public virtual EggRetrievalCycle EggRetrievalCycle { get; set; }

    public Guid TreatmentPlanId { get; set; }

    public virtual TreatmentPlan TreatmentPlan { get; set; }

    public string FertilizationMethod { get; set; } = "#NoData";

    public DateOnly FertilizationDate { get; set; }

    public int TotalEggsUsed { get; set; }

    public int? TotalEggsFertilized { get; set; } = 0;

    public int? TotalEmbryosFormed { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}

