using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.UseCase.DTOs.Patient;
using FertilityCare.UseCase.DTOs.PatientPartner;
using FertilityCare.UseCase.Interfaces;
using FertilityCare.UseCase.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        private readonly IPatientPartnerRepository _patientPartnerRepository;

        public PatientService(IPatientRepository patientRepository, IPatientPartnerRepository patientPartnerRepository)
        {
            _patientRepository = patientRepository;
            _patientPartnerRepository = patientPartnerRepository;
        }

        public async Task<IEnumerable<PatientDTO>> GetAllAsync()
        {
            var result = await _patientRepository.GetAllAsync();
            return result.Select(x => x.MapToPatientDTO()).ToList();
        }

        public async Task<PatientDTO> GetByIdAsync(Guid id)
        {
            var result = await _patientRepository.GetByIdAsync(id);
            return result.MapToPatientDTO();
        }

        public async Task<PatientPartnerDTO> GetPartnerByPatientId(Guid Id)
        {
            return null;
        }
    }
}
