using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class TreatmentProgressReport
{
    public Guid Id { get; set; }

    public Guid ServicePackagePlanId { get; set; }

    public Guid DoctorId { get; set; }

    public DateTime? ReportDate { get; set; }

    public string NextSteps { get; set; }

    public string? OverallAssessment { get; set; }

    public DateTime CreatedAt { get; set; }

}
