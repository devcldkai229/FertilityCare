using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

<<<<<<< HEAD
public interface IServicePackagePlanStepRepository : IBaseRepository<ServicePackagePlanStep, long>
=======
public interface IServicePackagePlanStepRepository : IBaseRepository<ServicePackagePlan, Guid>
>>>>>>> 8a9d6d21333bd116d89cc790a1916317ff1b146c
{
    Task<IEnumerable<ServicePackagePlanStep>> GetByPlanIdAsync(Guid planId);
    Task<ServicePackagePlanStep> GetCurrentStepAsync(Guid planId);
}
