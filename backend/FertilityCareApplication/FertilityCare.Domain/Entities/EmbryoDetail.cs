using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class EmbryoDetail
{
    public Guid Id { get; set; }

    public Guid EmbryoFertilizationId { get; set; }

    public string Grade { get; set; }

    public bool? IsViable { get; set; }

    public EmbryoStatus Status { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}
