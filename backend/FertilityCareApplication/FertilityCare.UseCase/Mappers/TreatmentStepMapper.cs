using FertilityCare.Domain.Entities;
using FertilityCare.UseCase.DTOs.TreamentServiceSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Mappers
{
    public static class TreatmentStepMapper
    {

        public static TreatmentStepDTO MapToTreatmentStepDTO(this TreatmentStep model)
        {
            return new TreatmentStepDTO
            {
                Id = model.Id,
                StepName = model.StepName,
                Description = model.Description,
                StepOrder = model.StepOrder,
                EstimatedDurationDays = model.EstimatedDurationDays,
            };
        }

    }
}