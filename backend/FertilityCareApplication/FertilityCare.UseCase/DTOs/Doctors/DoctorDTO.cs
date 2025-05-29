using FertilityCare.Domain.Enums;
using FertilityCare.UseCase.DTOs.DoctorSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.Doctors
{
    public class DoctorDTO
    {
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? Gender { get; set; }

        public string? DateOfBirth { get; set; }

        public string? Degree { get; set; }

        public string? Specialization { get; set; }

        public int? YearsOfExperience { get; set; }

        public string? Biography { get; set; }

        public string? Education { get; set; }

        public decimal? Rating { get; set; }

        public int? PatientsServed { get; set; }

        public bool? IsAcceptingPatients { get; set; }

        public string? Address { get; set; }

        public string? AvatarUrl { get; set; }

        public string? CreatedAt { get; set; }

        public List<DoctorScheduleDTO>? DoctorSchedules { get; set; }
    }
}
