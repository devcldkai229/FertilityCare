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

    public Guid TreatmentPlanId { get; set; }

    public virtual TreatmentPlan TreatmentPlan { get; set; }

    public DateTime PrescriptionDate { get; set; } = DateTime.Now;

    public string Note { get; set; } = "";

    public virtual List<PrescriptionItem> PrescriptionItems { get; set; }
}
