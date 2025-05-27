using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IServicePackagePlanExtensionRepository : IBaseRepository<ServicePackagePlanExtension>
{
    Task<IEnumerable<ServicePackagePlanExtension>> GetByPlanIdAsync(Guid planId);
    Task<bool> CompleteExtensionAsync(Guid id);
    Task<decimal> GetTotalExtraFeesAsync(Guid planId);
    Task<IEnumerable<ServicePackagePlanExtension>> GetPendingExtensionsAsync(Guid planId);
}
