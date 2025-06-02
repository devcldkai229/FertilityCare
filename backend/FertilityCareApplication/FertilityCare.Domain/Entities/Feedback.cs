using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class Feedback
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid TreatmentPlanId { get; set; }

    public decimal Rating { get; set; } 
    public decimal TreatmentQualityRating { get; set; } 

    public string Comment { get; set; }
    public bool IsDisplayed { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

 
    public virtual Patient Patient { get; set; }
    public virtual Doctor Doctor { get; set; }
    public virtual TreatmentPlan TreatmentPlan { get; set; }
}
