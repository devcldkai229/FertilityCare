using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.UseCase.DTOs.TreatmentServices;
using FertilityCare.UseCase.Interfaces;
using FertilityCare.UseCase.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Services
{
    public class PublicTreamentService : ITreatmentService
    {

        private readonly ITreatmentServiceRepository _treamentServiceRepository;

        private readonly ITreatmentCategoryRepository _treatmentCategoryRepository;

        public PublicTreamentService(ITreatmentServiceRepository treamentServiceRepository, ITreatmentCategoryRepository treatmentCategoryRepository)
        {
            _treamentServiceRepository = treamentServiceRepository;
            _treatmentCategoryRepository = treatmentCategoryRepository;
        }

        public async Task<TreatmentServiceDTO> CreateAsync(CreateTreatmentServiceRequestDTO request)
        {
            var treatmentModel = request.MapToTreatmentServiceModel(); 
            var existCategory = await _treatmentCategoryRepository.GetByIdAsync(treatmentModel.TreamentCategoryId);

            treatmentModel.TreamentCategory = existCategory;
            treatmentModel.TreatmentSteps = new List<TreatmentStep>();
            treatmentModel.CreatedAt = DateTime.Now;
            treatmentModel.IsActive = true;
            treatmentModel.UpdatedAt = null;
            
            await _treamentServiceRepository.CreateAsync(treatmentModel);
            return treatmentModel.MapToTreamentServiceDTO();
        }

        public async Task<IEnumerable<TreatmentServiceDTO>> GetAllAsync()
        {
            var result = await _treamentServiceRepository.GetAllAsync(); 
            return result.Select(x => x.MapToTreamentServiceDTO()).ToList();
        }

        public async Task<TreatmentServiceDTO> GetByIdAsync(Guid Id)
        {
           var loadedTreament = await _treamentServiceRepository.GetByIdAsync(Id);
           return loadedTreament.MapToTreamentServiceDTO();
        }
    }
}
