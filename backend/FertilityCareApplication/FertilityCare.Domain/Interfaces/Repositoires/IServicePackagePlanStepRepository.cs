using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

<<<<<<< HEAD
public interface IServicePackagePlanStepRepository : IBaseRepository<ServicePackagePlan, Guid>
=======

public interface IServicePackagePlanStepRepository : IBaseRepository<ServicePackagePlanStep, long>



>>>>>>> origin/features/thanghs
{
    Task<IEnumerable<ServicePackagePlanStep>> GetByPlanIdAsync(Guid planId);
    Task<ServicePackagePlanStep> GetCurrentStepAsync(Guid planId);
}
