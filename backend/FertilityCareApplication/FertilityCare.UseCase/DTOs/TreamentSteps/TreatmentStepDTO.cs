using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.TreamentServiceSteps
{
    public class TreatmentStepDTO
    {

        public long? Id { get; set; }

        public string? StepName { get; set; }

        public string? Description { get; set; }

        public int? StepOrder { get; set; }

        public int? EstimatedDurationDays { get; set; }

    }
}
