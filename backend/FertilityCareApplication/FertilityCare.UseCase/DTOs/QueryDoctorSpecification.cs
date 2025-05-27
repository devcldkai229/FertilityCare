using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs
{
    public class QueryDoctorSpecification
    {
        public bool IsAcceptingPatients { get; set; } = true;

        public string? Specialization { get; set; }

        public int? YearsOfExperience { get; set; }

        public decimal? MinRating { get; set; }

        public decimal? MinPatientsServed { get; set; }

        public string? SortBy { get; set; } = "Rating";

        public bool IsDescending { get; set; } = true;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 4;

    }
}
