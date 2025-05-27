using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class EggRetrievalCycle
{
    public Guid Id { get; set; }

    public Guid ServicePackagePlanId { get; set; }

    public virtual ServicePackagePlan ServicePackagePlan { get; set; }

    public int? CycleNumber { get; set; }

    public DateTime RetrievalDate { get; set; }

    public int TotalEggsRetrieved { get; set; }

    public int? MatureEggs { get; set; }

    public int? ImmatureEggs { get; set; }

    public int? AbnormalEggs { get; set; }

    public Guid? DoctorId { get; set; }

    public virtual Doctor? Doctor { get; set; } 

    public string? DoctorNotes { get; set; }


}
