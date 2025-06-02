using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class EmbryoTransfer
{
    public Guid Id { get; set; }

    public Guid EmbryoDetailId { get; set; }

    public virtual EmbryoDetail EmbryoDetail { get; set; }

    public Guid TreatmentPlanId { get; set; }

    public virtual TreatmentPlan TreatmentPlan { get; set; }

    public bool IsFrozenTransfer { get; set; } = false;

    public DateTime TransferDate { get; set; } = DateTime.Now;

    public bool IsSuccessful { get; set; } = false;

    public string PregnancyResultNote { get; set; } = "";

    public Guid DoctorId { get; set; }

    public decimal FeeCharged { get; set; }

    public string Note { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }



}
