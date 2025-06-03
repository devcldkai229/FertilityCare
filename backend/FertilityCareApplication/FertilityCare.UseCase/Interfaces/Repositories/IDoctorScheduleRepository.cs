using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IDoctorScheduleRepository : IBaseRepository<DoctorSchedule, long>
    {
        Task<IEnumerable<DoctorSchedule>> FindDoctorScheduleByDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<DoctorSchedule>> FindAllDoctorScheduleByAvailableSchedulesAsync();
    }
}
