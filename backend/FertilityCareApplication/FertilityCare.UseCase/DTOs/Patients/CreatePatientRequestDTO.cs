using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.Patient
{
    public class CreatePatientRequestDTO
    {
        public Guid UserProfileId { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? Gender { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public string? MedicalHistory { get; set; }

        public string? PartnerFullName { get; set; }

        public DateOnly? PartnerDateOfBirth { get; set; }

        public string? PartnerPhoneNumber { get; set; }

        public string? PartnerEmail { get; set; }
    }
}
