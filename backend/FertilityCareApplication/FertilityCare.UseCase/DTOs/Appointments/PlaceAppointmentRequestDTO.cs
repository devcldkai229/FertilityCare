using FertilityCare.UseCase.Validators.Customs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.Appointments
{
    public class PlaceAppointmentRequestDTO
    {

        public string UserId { get; set; }

        public string TreatmentServiceId { get; set; }

        public string DoctorId { get; set; }

        public long DoctorScheduleId {  get; set; }
        
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        [EmailAddress]
        public string? BookingEmail { get; set; }

        [Phone]
        public string? BookingPhone { get; set; }

        [ValidAge]
        public DateOnly? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public string? PartnerFullName { get; set; }

        [ValidAge]
        public DateOnly? PartnerDateOfBirth { get; set; }

        [Phone]
        public string? PartnerPhoneNumber { get; set; }

        [EmailAddress]
        public string? PartnerEmail { get; set; }

        public string? Note { get; set; }


    }
}
