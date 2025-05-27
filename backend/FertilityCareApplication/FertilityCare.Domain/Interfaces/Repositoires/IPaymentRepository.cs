using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<Payment> GetByPaymentCodeAsync(string paymentCode);
    Task<IEnumerable<Payment>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Payment>> GetByPlanIdAsync(Guid planId);
    Task<bool> UpdateStatusAsync(Guid id, string status);
    Task<bool> ProcessRefundAsync(Guid id, decimal refundAmount, string reason);
    Task<IEnumerable<Payment>> GetByStatusAsync(string status);
    Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<Payment>> GetPendingPaymentsAsync();
}
