using FertilityCare.Domain.DTOs;
using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IDoctorScheduleRepository : IBaseRepository<DoctorSchedule, long>
{
    Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAsync(Guid doctorId);
    Task<IEnumerable<DoctorSchedule>> GetAvailableSchedulesAsync();
    Task<IEnumerable<DoctorSchedule>> GetAllBySpecificationAsync(QueryDoctorScheduleSpecification query);
}
