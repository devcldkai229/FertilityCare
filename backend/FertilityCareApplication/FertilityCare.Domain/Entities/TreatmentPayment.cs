using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class TreatmentPayment
{
    public Guid Id { get; set; }

    public Guid UserProfileId { get; set; }

    public virtual UserProfile UserProfile { get; set; }

    public long TreatmentPlanStepId { get; set; }

    public virtual TreatmentPlanStep TreatmentPlanStep { get; set; }

    public string PaymentCode { get; set; } = "#NoData";

    public decimal Amount { get; set; }

    public Guid PaymentMethodId { get; set; }

    public virtual PaymentMethod PaymentMethod { get; set; }

    public string TransactionCode { get; set; } = "#NoData";

    public DateTime PaymentDate { get; set; } = DateTime.Now;

    public PaymentStatus Status { get; set; }

    public string Note { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

}
