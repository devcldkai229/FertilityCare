using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities
{
    public class TestResult
    {
        public long Id { get; set; }
        
        public Guid TreatmentPlanId { get; set; }

        public virtual TreatmentPlan TreatmentPlan { get; set; }

        public string TestName { get; set; } = "undefined";
        
        public DateTime TestDate { get; set; }

        public string? Note { get; set; } = "";

        public string Result { get; set; } = "undefined";
    }
}
