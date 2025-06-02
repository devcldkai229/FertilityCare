using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Enums;

public enum PaymentStatus
{
    Pending = 1,

    Partial = 2,

    Completed = 3,

    Refunded = 4,

    Failed = 5
}
