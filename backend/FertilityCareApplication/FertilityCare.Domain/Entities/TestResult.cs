using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class TestResult
{
    public long Id { get; set; }

    public Guid ServicePackagePlanId { get; set; }

    public virtual ServicePackagePlan ServicePackagePlan { get; set; } = null!;

    public string TestName { get; set; } = null!;

    public string? TestCategory { get; set; }

    public string? ResultValue { get; set; }

    public string? Note { get; set; }

    public DateTime? TestDate { get; set; }
}
