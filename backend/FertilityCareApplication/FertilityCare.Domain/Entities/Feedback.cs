using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

using FertilityCare.Domain.Enums;
using System;

public class Feedback
{
    public Guid Id { get; set; }

    public Guid UserProfileId { get; set; }

    public Guid DoctorId { get; set; }

    public Guid ServicePackagePlanId { get; set; }

    public decimal Rating { get; set; }

    public string? Comment { get; set; }

    public decimal? TreatmentQualityRating { get; set; }

    public decimal? PrivacyRating { get; set; }

    public bool IsDisplayed { get; set; } = false;

    public FeedbackStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public virtual UserProfile? UserProfile { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual ServicePackagePlan? ServicePackagePlan { get; set; }

}

