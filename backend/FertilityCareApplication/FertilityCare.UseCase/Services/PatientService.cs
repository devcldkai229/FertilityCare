using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.UseCase.DTOs.Patient;
using FertilityCare.UseCase.Interfaces;
using FertilityCare.UseCase.Mappers;
using Microsoft.AspNetCore.Identity;
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

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientDTO> CreateAsync(Patient request)
        {
            var result = await _patientRepository.CreateAsync(request);
            return result.MapToPatientDTO();
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
    }
}
