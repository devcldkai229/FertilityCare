using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class ServicePackagePlanExtension
{
    public long Id { get; set; }

    public Guid ServicePackagePlanId { get; set; }

    public string StepName { get; set; }

    public string? Reason { get; set; }

    public string? Note { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? ExtraFee { get; set; }

    public bool? IsComplete { get; set; }

    public DateTime? CompletedAt { get; set; }

}
