using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class FrozenEmbryoStorage
{
    public Guid Id { get; set; }

    public Guid EmbryoDetailId { get; set; }

    public DateTime StorageStartDate { get; set; }

    public DateTime? StorageEndDate { get; set; }

    public string StorageTank { get; set; } = null!;

    public FreezeMethodType FreezeMethod { get; set; }

    public decimal? MonthlyStorageFee { get; set; }

    public FrozenEmbryoStorageStatus Status { get; set; }

    public bool? SurvivalAfterThaw { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual EmbryoDetail EmbryoDetail { get; set; } = null!;
}
