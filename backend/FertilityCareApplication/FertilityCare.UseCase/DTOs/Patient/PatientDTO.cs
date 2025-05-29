using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.Patient
{
    public class PatientDTO
    {
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? Gender { get; set; }

        public string? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Province { get; set; }

        public string? Country { get; set; }

        public string? AvatarUrl { get; set; }

        public string? MedicalHistory { get; set; }

        public string? FertilityDiagnosis { get; set; }

        public string? AllergiesNotes { get; set; }

        public string? BloodType { get; set; }

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public string? MaritalStatus { get; set; }

        public string? PartnerFullName { get; set; }

        public string? CreatedAt { get; set; }

    }
}