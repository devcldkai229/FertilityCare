using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.DTOs
{
    public class QueryDoctorScheduleSpecification
    {
        public DateOnly? WorkDate { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 4;
    }
}
