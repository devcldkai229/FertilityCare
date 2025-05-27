using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IPaymentRepository : IBaseRepository<Payment, Guid>
{
    Task<Payment> GetByPaymentCodeAsync(string paymentCode);
    Task<IEnumerable<Payment>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Payment>> GetByPlanIdAsync(Guid planId);
    Task<bool> UpdateStatusAsync(Guid id, string status);
    Task<IEnumerable<Payment>> GetByStatusAsync(string status);
}
