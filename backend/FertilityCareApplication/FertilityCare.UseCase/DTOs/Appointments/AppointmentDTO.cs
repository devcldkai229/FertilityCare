using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.Appointments
{
    public class AppointmentDTO
    {
        public string? Id {  get; set; }

        public string? PatientId { get; set; }  

        public string? PatientName { get; set; }

        public string? DoctorId { get; set; }

        public string? DoctorName { get; set;}

        public string? TreatmentServiceId { get; set; }

        public string? TreatmentServiceName { get; set; }   

        public string? BookingEmail { get; set; }

        public string? BookingPhone { get; set; }

        public string? AppointmentDate { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public string? Purpose { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }

    }
}
