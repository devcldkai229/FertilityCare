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

        public string? MedicalHistory { get; set; }

        public string? FertilityDiagnosis { get; set; }

        public string? AllergiesNotes { get; set; }

        public string? BloodType { get; set; }

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public string? MaritalStatus { get; set; }

        public string FullName { get; set; } = null!;

        public string? Gender { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? BloodTypePartner { get; set; }

        public string? MedicalHistoryPartner { get; set; }

        public string? ContactNumberPartner { get; set; }

        public string? EmailPartner { get; set; }

        public string? Note { get; set; }
    }
}
