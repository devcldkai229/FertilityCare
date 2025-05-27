using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class EmbryoFertilization
{
    public Guid Id { get; set; }

    public Guid EggRetrievalCycleId { get; set; }

    public virtual EggRetrievalCycle EggRetrievalCycle { get; set; }

    public DateOnly FertilizationDate { get; set; }

    public int TotalEggsUsed { get; set; }

    public int? TotalEggsFertilized { get; set; }

    public int? TotalEmbryosFormed { get; set; }

    public Guid DoctorId { get; set; }

    public string? EmbryologistNotes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
}
