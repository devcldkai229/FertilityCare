using FertilityCare.UseCase.DTOs.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Interfaces
{
    public interface IDoctorService
    {

        Task<DoctorDTO> GetByIdAsync(Guid id);

        Task<IEnumerable<DoctorDTO>> GetAllAsync();

        Task<DoctorDTO> CreateAsync(CreateDoctorRequestDTO request);
    }
}
