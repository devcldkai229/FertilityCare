using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities
{
    public class TreatmentPlan
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public Guid DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public Guid TreatmentServiceId { get; set; }

        public virtual TreatmentService TreatmentService { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public TreatmentPlanStatus Status { get; set; } = TreatmentPlanStatus.Planned;

        public decimal? TotalPrice { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public string? Note { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public virtual List<TreatmentPlanStep>? TreatmentPlanSteps { get; set; } = null;
    }
}
