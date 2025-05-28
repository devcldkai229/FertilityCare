using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.TreatmentServices
{
    public class CreateTreatmentServiceRequestDTO
    {

        public string CategoryId { get; set; }

        public string ServiceName { get; set; }

        public string? Description { get; set; }

        public decimal BasicPrice { get; set; }

        public int? Duration { get; set; }

        public decimal? SucessRate { get; set; }

        public int? MinAge { get; set; }    

        public int? MaxAge { get; set; }

        public string? RecommendedFor { get; set; }

        public string? Contraindications { get; set; }

    }
}
