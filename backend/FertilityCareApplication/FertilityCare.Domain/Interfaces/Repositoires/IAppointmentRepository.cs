using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IAppointmentRepository : IBaseRepository<Appointment, Guid>
{
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId);
    Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<Appointment> CancelAppointmentAsync(Guid id, string reason);
    Task<IEnumerable<Appointment>> GetTodayAppointmentsAsync(Guid doctorId);
    Task<IEnumerable<Appointment>> GetByTreatmentServiceAsync(Guid treatmentServiceId);
}
