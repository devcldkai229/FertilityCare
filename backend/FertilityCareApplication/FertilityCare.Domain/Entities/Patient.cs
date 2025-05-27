using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }

    public Guid UserProfileId { get; set; }

    public virtual UserProfile UserProfile { get; set; }

    public string? MedicalHistory { get; set; }

    public string? FertilityDiagnosis { get; set; }

    public string? AllergiesNotes { get; set; }

    public string? BloodType { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public string? MaritalStatus { get; set; }

    public Guid? PatientParnerId { get; set; }

    public string? Note { get; set; }
}
