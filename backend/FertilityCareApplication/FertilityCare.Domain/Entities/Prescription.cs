using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class Prescription
{
    public Guid Id { get; set; }

    public Guid? ServicePackagePlanId { get; set; }

    public DateTime PrescriptionDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Note { get; set; }

    public PrescriptionStatus Status { get; set; }

    public virtual List<PrescriptionItem> PrescriptionItems { get; set; }
}
