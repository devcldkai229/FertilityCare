using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IAppointmentReminderRepository : IBaseRepository<AppointmentReminder, long>
{
    Task<IEnumerable<AppointmentReminder>> GetByAppointmentIdAsync(Guid appointmentId);
    Task<IEnumerable<AppointmentReminder>> GetByPatientIdAsync(Guid patientId);
    Task<bool> MarkAsSentAsync(long id);
}
