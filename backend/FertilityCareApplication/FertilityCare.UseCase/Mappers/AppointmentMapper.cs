using FertilityCare.Domain.Entities;
using FertilityCare.UseCase.DTOs.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Mappers
{
    public static class AppointmentMapper
    {

        public static AppointmentDTO MapToAppointmentDTO(this Appointment model)
        {
            return new AppointmentDTO
            {
                Id = model.Id.ToString(),
                PatientId = model.PatientId.ToString(),
                PatientName = String.Join(' ', new string[] { model.Patient.UserProfile.FirstName,
                                                              model.Patient.UserProfile.MiddleName, 
                                                              model.Patient.UserProfile.LastName}),
                DoctorId = model.DoctorId.ToString(),
                DoctorName = String.Join(' ', new string[] {  model.Patient.UserProfile.FirstName,
                                                              model.Patient.UserProfile.MiddleName,
                                                              model.Patient.UserProfile.LastName}),
                TreatmentServiceId = model.TreatmentServiceId.ToString(),
                TreatmentServiceName = model.TreatmentService.Name,
                BookingEmail = model.BookingEmail,
                BookingPhone = model.BookingPhone,
                AppointmentDate = model.AppointmentDate.ToString("dd/MM/yyyy"),
                StartTime = model.StartTime.ToString(),
                EndTime = model.EndTime.ToString(),
                Purpose = model.Purpose,
                Status = model.Status.ToString(),
            };
        }
    }
}
