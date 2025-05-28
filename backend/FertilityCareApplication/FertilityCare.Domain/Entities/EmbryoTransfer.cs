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

    public bool IsFrozenTransfer { get; set; }

    public DateTime TransferDate { get; set; }

    public bool? IsSuccessful { get; set; }

    public string? PregnancyResultNote { get; set; }

    public Guid DoctorId { get; set; }

    public decimal FeeCharged { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }



}
