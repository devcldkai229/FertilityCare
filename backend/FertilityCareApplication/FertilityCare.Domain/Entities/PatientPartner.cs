using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class PatientPartner
{
    public Guid Id { get; set; }

    public string? FullName { get; set; } = null!;

    public Gender? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? BloodType { get; set; }

    public string? MedicalHistory { get; set; }

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}
