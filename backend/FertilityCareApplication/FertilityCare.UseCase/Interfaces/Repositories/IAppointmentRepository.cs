using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IAppointmentRepository : IBaseRepository<Appointment, Guid>
    {
        Task<IEnumerable<Appointment>> FindAppointmentByPatientIdAsync(Guid patientId);
        Task<IEnumerable<Appointment>> FindAppointmentByDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<Appointment>> FindAppointmentByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Appointment>> GetTodayAppointmentsAsync(Guid doctorId);
    }
}
