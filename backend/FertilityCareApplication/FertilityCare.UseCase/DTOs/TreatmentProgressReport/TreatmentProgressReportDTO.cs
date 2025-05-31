using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs.TreatmentProgressReport
{
    public class TreatmentProgressReportDTO
    {
        public Guid Id { get; set; }

        public Guid ServicePackagePlanId { get; set; }
        public string ServicePackagePlanName { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; }  

        public DateTime? ReportDate { get; set; }

        public string NextSteps { get; set; }

        public string? OverallAssessment { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
