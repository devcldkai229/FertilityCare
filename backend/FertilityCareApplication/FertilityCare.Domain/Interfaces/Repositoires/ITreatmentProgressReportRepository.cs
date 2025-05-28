using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface ITreatmentProgressReportRepository : IBaseRepository<TreatmentProgressReport, Guid>
{
    Task<IEnumerable<TreatmentProgressReport>> GetByPlanIdAsync(Guid planId);
    Task<IEnumerable<TreatmentProgressReport>> GetByDoctorIdAsync(Guid doctorId);
    Task<TreatmentProgressReport> GetLatestReportAsync(Guid planId);
    Task<IEnumerable<TreatmentProgressReport>> GetByDateRangeAsync(Guid planId, DateTime startDate, DateTime endDate);
}
