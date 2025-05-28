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

        public PublicTreamentService(ITreatmentServiceRepository treamentServiceRepository)
        {
            _treamentServiceRepository = treamentServiceRepository;
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
