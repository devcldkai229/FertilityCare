using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IServicePackagePlanRepository : IBaseRepository<ServicePackagePlan, Guid>
{
    Task<IEnumerable<ServicePackagePlan>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<ServicePackagePlan>> GetByDoctorIdAsync(Guid doctorId);
    Task<IEnumerable<ServicePackagePlan>> GetByServiceIdAsync(Guid serviceId);
    Task<bool> UpdateStatusAsync(Guid id, string status);
    Task<bool> UpdatePaymentStatusAsync(Guid id, string paymentStatus);
    Task<IEnumerable<ServicePackagePlan>> GetByStatusAsync(string status);
    Task<IEnumerable<ServicePackagePlan>> GetCompletedByPatientAsync(Guid patientId);
    Task<decimal> GetTotalRevenueAsync();
    Task<int> GetActivePlansCountAsync();
}
