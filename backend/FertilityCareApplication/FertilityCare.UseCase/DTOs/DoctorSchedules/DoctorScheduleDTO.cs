using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.DoctorSchedules
{
    public class DoctorScheduleDTO
    {

        public long Id { get; set; }

        public string? WorkDate { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public bool? IsAvailable { get; set; }

        public int? MaxAppointments { get; set; }

        public string? Note { get; set; }

    }
}
