using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }

    public Guid UserProfileId { get; set; }

    public virtual UserProfile UserProfile { get; set; }

    public Guid? ServicePackagePlanId { get; set; }

    public virtual ServicePackagePlan? ServicePackagePlan { get; set; }

    public string? PaymentCode { get; set; }

    public decimal Amount { get; set; }

    public Guid PaymentMethodId { get; set; }

    public virtual PaymentMethod PaymentMethod { get; set; }

    public string? TransactionCode { get; set; }

    public DateTime PaymentDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal? RefundAmount { get; set; }

    public string? RefundReason { get; set; }

    public DateTime? RefundDate { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}
