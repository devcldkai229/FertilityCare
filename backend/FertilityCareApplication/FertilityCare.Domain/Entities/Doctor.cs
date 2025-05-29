using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class Doctor
{

    public Guid Id { get; set; }

    public Guid UserProfileId { get; set; }

    public virtual UserProfile UserProfile { get; set; }

    public string? Degree { get; set; } = "#NoData";

    public string? Specialization { get; set; } = "#NoData";

    public int? YearsOfExperience { get; set; } = 0;

    public string? Biography { get; set; } = "#NoData";

    public string? Education { get; set; } = "#NoData";

    public decimal? Rating { get; set; } = 0;

    public int? PatientsServed { get; set; } = 0;

    public bool? IsAcceptingPatients { get; set; } = true;

    public virtual List<DoctorSchedule>? DoctorSchedules { get; set; } = null;

}
    