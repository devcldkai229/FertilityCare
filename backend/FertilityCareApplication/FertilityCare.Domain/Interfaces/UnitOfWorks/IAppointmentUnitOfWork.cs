using FertilityCare.Domain.Interfaces.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.UnitOfWorks;

public interface IAppointmentUnitOfWork
{
    IAppointmentRepository _appointmentRepository { get; }

    IAppointmentReminderRepository _appointmentReminderRepository { get; }

    IDoctorScheduleRepository _doctorScheduleRepository { get; }

}
