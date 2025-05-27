using FertilityCare.Domain.Entities;
using FertilityCare.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Data;

public class FertilityCareDBContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{

    private static readonly string DB_CONNECTION_STRING = "Server=localhost,1433;Database=FeritilyCareDB;User Id=sa;Password=12345;TrustServerCertificate=True;Encrypt=false";   

    private static readonly ILoggerFactory _logger = LoggerFactory.Create(
        options =>
        {
            options.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information)
            .AddConsole();
        }
    ); 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(DB_CONNECTION_STRING);

        optionsBuilder.UseLazyLoadingProxies();

        optionsBuilder.UseLoggerFactory(_logger);
    }

    public DbSet<UserProfile> UserProfiles { get; set; }

    public DbSet<PatientPartner> PatientPartners { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

    public DbSet<TreatmentCategory> TreatmentCategories { get; set; }

    public DbSet<TreatmentService> TreatmentServices { get; set; }

    public DbSet<TreatmentStep> TreatmentSteps { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<ServicePackagePlan> ServicePackagePlans { get; set; }

    public DbSet<ServicePackagePlanStep> ServicePackagePlanSteps { get; set; }

    public DbSet<ServicePackagePlanExtension> ServicePackagePlanExtensions { get; set; }

    public DbSet<TestResult> TestResults { get; set; }

    public DbSet<TreatmentProgressReport> TreatmentProgressReports { get; set; }

    public DbSet<Prescription> Prescriptions { get; set; }

    public DbSet<PrescriptionItem> PrescriptionItems { get; set; }

    public DbSet<MonitorReminder> MonitorReminders { get; set; }
    
    public DbSet<AppointmentReminder> AppointmentReminders { get; set; }

    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<MediaFile> MediaFiles { get; set; }

    public DbSet<Feedback> Feedbacks { get; set; }

    public DbSet<Blog> Blogs { get; set; }

    public DbSet<EggRetrievalCycle> EggRetrievalCycles { get; set; }

    public DbSet<EmbryoFertilization> EmbryoFertilizations { get; set; }

    public DbSet<EmbryoDetail> EmbryoDetails { get; set; }

    public DbSet<FrozenEmbryoStorage> FrozenEmbryoStorages { get; set; }
}
