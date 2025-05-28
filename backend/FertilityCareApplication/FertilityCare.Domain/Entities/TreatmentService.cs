using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class TreatmentService
{
    public Guid Id { get; set; }

    public Guid TreamentCategoryId { get; set; }

    public virtual TreatmentCategory TreamentCategory { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public decimal BasicPrice { get; set; }

    public int? Duration { get; set; } 

    public bool IsActive { get; set; }

    public decimal? SuccessRate { get; set; }

    public int? MinAge { get; set; }

    public int? MaxAge { get; set; }

    public string? RecommendedFor { get; set; }

    public string? Contraindications { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual List<TreatmentStep> TreatmentSteps { get; set; }

    public override string ToString()
    {
        return $"TreatmentService: Id={Id}, " +
               $"TreamentCategoryId={TreamentCategoryId}, " +
               $"Name={Name}, " +
               $"Description={Description ?? "N/A"}, " +
               $"BasicPrice={BasicPrice}, " +
               $"Duration={(Duration.HasValue ? Duration.Value.ToString() : "N/A")}, " +
               $"IsActive={IsActive}, " +
               $"SuccessRate={(SuccessRate.HasValue ? SuccessRate.Value.ToString() : "N/A")}, " +
               $"MinAge={(MinAge.HasValue ? MinAge.Value.ToString() : "N/A")}, " +
               $"MaxAge={(MaxAge.HasValue ? MaxAge.Value.ToString() : "N/A")}, " +
               $"RecommendedFor={RecommendedFor ?? "N/A"}, " +
               $"Contraindications={Contraindications ?? "N/A"}, " +
               $"CreatedAt={CreatedAt}, " +
               $"UpdatedAt={(UpdatedAt.HasValue ? UpdatedAt.Value.ToString() : "N/A")}, " +
               $"TreatmentStepsCount={TreatmentSteps?.Count ?? 0}";
    }


}
