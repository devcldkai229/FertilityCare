using FertilityCare.UseCase.DTOs.TreamentServiceSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.TreatmentServices
{
    public  class TreatmentServiceDTO
    {
        public string Id { get; set; }

        public string? CategoryId { get; set; }

        public string? Name { get; set; }

        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public decimal? BasicPrice { get; set; }

        public string? FormattedPrice { get; set; }

        public int? Duration { get; set; }

        public bool IsActive { get; set; }

        public decimal? SuccessRate { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        public string? RecommendedFor { get; set; }

        public string? Contraindications { get; set; }

        public string? CreatedAt { get; set; }

        public string? UpdateAt { get; set; }

        public List<TreatmentStepDTO>? TreamentSteps { get; set; }
    }
}
