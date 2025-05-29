using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.Doctors
{
    public class CreateDoctorRequestDTO
    {
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string? PhoneNumber { get; set; } = "#NoData";

        public string? FirstName { get; set; } = "#NoData";

        public string? MiddleName { get; set; } = "#NoData";

        public string? LastName { get; set; } = "#NoData";

        public string? Gender { get; set; } = "Unknown";

        public string? Address { get; set; } = "#NoData";

        public DateOnly? DateOfBirth { get; set; } = null;

        public string? Degree { get; set; } = "#NoData";

        public string? Specialization { get; set; } = "#NoData";

    }
}
