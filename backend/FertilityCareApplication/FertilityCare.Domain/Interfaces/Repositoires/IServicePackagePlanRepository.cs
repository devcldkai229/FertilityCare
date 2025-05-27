using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IServicePackagePlanRepository : IBaseRepository<ServicePackagePlan, Guid>
{
    
    Task<bool> UpdateStatusAsync(Guid id, string status);
    Task<bool> UpdatePaymentStatusAsync(Guid id, string paymentStatus);
    
    
    Task<int> GetActivePlansCountAsync();
}
