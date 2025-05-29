using FertilityCare.UseCase.DTOs.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Interfaces
{
    public interface IAppointmentService
    {

        Task<AppointmentDTO> PlaceAppointmentLockAsync(PlaceAppointmentRequestDTO request);

        Task<AppointmentDTO> UpdateAppointmentInfoAsync(UpdateAppointmentRequestDTO request);

        Task<IEnumerable<AppointmentDTO>> GetAppointmentByPatientIdAsync(Guid patientId);

        Task<IEnumerable<AppointmentDTO>> GetAppointmentByTreatmentServiceIdAsync(Guid treatmentServiceId);

        Task<IEnumerable<AppointmentDTO>> GetAppointmentByDoctorScheduleIdAsync(long scheduleId);

        Task<IEnumerable<AppointmentDTO>> GetAppointmentByDoctorIdAsync(Guid doctorId);

    }
}
