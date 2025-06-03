using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface ITestResultRepository : IBaseRepository<TestResult, long>
    {
        Task<IEnumerable<TestResult>> FindByTreatmentPlanIdAsync(Guid treatmentPlanId);
        Task<TestResult?> FindLatestByTreatmentPlanIdAsync(Guid treatmentPlanId);
        Task<IEnumerable<TestResult>> SearchByTestNameAsync(string keyword);
        Task<IEnumerable<TestResult>> FindDateRangeAsync(DateTime from, DateTime to);
    }
}
