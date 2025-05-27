using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class PrescriptionItem
{
    public long Id { get; set; }

    public Guid PrescriptionId { get; set; }

    public string MedicationName { get; set; }

    public string? Dosage { get; set; }

    public int? Quantity { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? SpecialInstructions { get; set; }


}
