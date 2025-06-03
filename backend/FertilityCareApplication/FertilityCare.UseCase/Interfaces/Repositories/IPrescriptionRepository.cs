using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IPrescriptionRepository : IBaseRepository<Prescription, Guid>
    {
        Task<IEnumerable<Prescription>> FindPrescriptionsByPatientIdAsync(Guid patientId);
        Task<IEnumerable<Prescription>> FindPrescriptionsByDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<Prescription>> FindPrescriptionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<Prescription> FindPrescriptionByIdAsync(Guid prescriptionId);
        Task<IEnumerable<Prescription>> SearchPrescriptionsByKeywordAsync(string keyword);
        Task<IEnumerable<Prescription>> GetByTreatmentPlanIdAsync(Guid treatmentPlanId);
        Task<Prescription?> GetLatestByTreatmentPlanIdAsync(Guid treatmentPlanId);
    }
}
