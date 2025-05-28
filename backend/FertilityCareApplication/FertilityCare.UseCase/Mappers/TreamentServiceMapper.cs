using FertilityCare.Domain.Entities;
using FertilityCare.UseCase.DTOs.TreamentServiceSteps;
using FertilityCare.UseCase.DTOs.TreatmentServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Mappers
{
    public static class TreamentServiceMapper
    {
        public static TreatmentServiceDTO MapToTreamentServiceDTO(this TreatmentService model)
        {
            return new TreatmentServiceDTO
            {
                Id = model.Id.ToString(),
                CategoryId = model.TreamentCategoryId.ToString(),
                Name = model.Name,
                CategoryName = model.TreamentCategory.Name,
                Description = model.Description,
                BasicPrice = model.BasicPrice,
                FormattedPrice = model.BasicPrice.ToString("C", new CultureInfo("vi-VN")),
                Duration = model.Duration,
                IsActive = model.IsActive,
                SuccessRate = model.SuccessRate,
                MinAge = model.MinAge,
                MaxAge = model.MaxAge,
                RecommendedFor = model.RecommendedFor,
                Contraindications = model.Contraindications,
                CreatedAt = model.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                UpdateAt = model.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),
                TreamentSteps = model.TreatmentSteps.Select(x => x.MapToTreatmentStepDTO()).ToList()
            };
        }

    }
}