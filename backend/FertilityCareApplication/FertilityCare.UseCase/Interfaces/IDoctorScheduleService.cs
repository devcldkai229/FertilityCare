using FertilityCare.UseCase.DTOs.DoctorSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Interfaces
{
    public interface IDoctorScheduleService
    {
        Task<DoctorScheduleDTO> AddWorkScheduleAsync(CreateDoctorScheduleRequestDTO request);

        Task<IEnumerable<DoctorScheduleDTO>> GetWorkScheduleByDoctorIdAsync(Guid doctorId);

        Task<DoctorScheduleDTO> GetByIdAsync(long id);
    }
}
