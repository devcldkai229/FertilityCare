using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Enums;

public enum ServicePlanStepStatus
{
    Pending = 1,

    InProgress = 2, 

    Completed = 3, 

    Skipped = 4, 

    Failed = 5
}       
