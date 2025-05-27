using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IServicePackagePlanStepRepository : IBaseRepository<ServicePackagePlan>
{
    Task<IEnumerable<ServicePackagePlanStep>> GetByPlanIdAsync(Guid planId);
    Task<IEnumerable<ServicePackagePlanStep>> GetPendingStepsAsync(Guid planId);
    Task<IEnumerable<ServicePackagePlanStep>> GetCompletedStepsAsync(Guid planId);
    Task<IEnumerable<ServicePackagePlanStep>> GetOverdueStepsAsync();
    Task<ServicePackagePlanStep> GetCurrentStepAsync(Guid planId);
}
