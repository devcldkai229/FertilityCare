using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class EggRetrievalCycle
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TreatmentPlanId { get; set; }

    public virtual TreatmentPlan TreatmentPlan { get; set; }

    public int CycleNumber { get; set; } = 1;

    public DateTime RetrievalDate { get; set; } = DateTime.Now;

    public int TotalEggsRetrieved { get; set; } = 0;

    public int MatureEggs { get; set; } = 0;

    public int ImmatureEggs { get; set; } = 0;

    public int AbnormalEggs { get; set; } = 0;

    public string Note { get; set; } = "";


}
