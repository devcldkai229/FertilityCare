using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class FrozenEmbryoStorage
{
    public Guid Id { get; set; }

    public Guid EmbryoDetailId { get; set; }

    public virtual EmbryoDetail EmbryoDetail { get; set; }

    public Guid TreatmentPlanId { get; set; }

    public virtual TreatmentPlan TreatmentPlan { get; set; }

    public DateTime StorageStartDate { get; set; }

    public DateTime? StorageEndDate { get; set; }

    public string StorageTank { get; set; } = null!;

    public FreezeMethod FreezeMethod { get; set; }

    public decimal? MonthlyStorageFee { get; set; }

    public StorageStatus Status { get; set; } = StorageStatus.Active;

    public bool SurvivalAfterThaw { get; set; } = true;

    public string Note { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

}
