using FertilityCare.Domain.Entities;
using FertilityCare.UseCase.DTOs.Patient;
using FertilityCare.UseCase.DTOs.PatientPartner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDTO> GetByIdAsync(Guid id);

        Task<IEnumerable<PatientDTO>> GetAllAsync();

        Task<PatientPartnerDTO> GetPartnerByPatientId(Guid id);



    }
}
