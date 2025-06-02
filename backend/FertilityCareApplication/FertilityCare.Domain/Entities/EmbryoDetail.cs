using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class EmbryoDetail
{
    public Guid Id { get; set; }

    public Guid EmbryoFertilizationId { get; set; }

    public virtual EmbryoFertilization EmbryoFertilization { get; set; }

    public Guid TreatmentPlanId { get; set; }

    public virtual TreatmentPlan TreatmentPlan { get; set; }

    public string Grade { get; set; }

    public bool IsViable { get; set; } = true;

    public EmbryoStatus Status { get; set; } = EmbryoStatus.Available;

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

}
