using FertilityCare.UseCase.Validators.Customs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.DoctorSchedules
{
    public class CreateDoctorScheduleRequestDTO
    {
        public string DoctorId { get; set; }

        [FutureDateOnly]
        public DateOnly WorkDate { get; set; }

        public TimeOnly StartTime { get; set; }

        [ValidTimeRange("StartTime")]
        public TimeOnly EndTime { get; set; }

        public string? Note { get; set; } = "#NoData";
    }
}
