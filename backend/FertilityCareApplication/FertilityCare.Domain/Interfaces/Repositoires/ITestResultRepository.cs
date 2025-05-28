using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface ITestResultRepository : IBaseRepository<TestResult, long>
{
    Task<IEnumerable<TestResult>> GetByPlanIdAsync(Guid planId);
    Task<IEnumerable<TestResult>> GetByDateRangeAsync(Guid planId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<TestResult>> GetLatestResultsAsync(Guid planId, int count);
    Task<IEnumerable<TestResult>> SearchByTestNameAsync(string testName);
}
