using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface ITreatmentPaymentRepository : IBaseRepository<TreatmentPayment, Guid>
    {
        Task<IEnumerable<TreatmentPayment>> FindByTreatmentPlanStepIdAsync(long stepId);
        Task<IEnumerable<TreatmentPayment>> FindByUserProfileIdAsync(Guid userId);
        Task<TreatmentPayment?> FindByTransactionCodeAsync(string transactionCode);
        Task<TreatmentPayment?> FindByPaymentCodeAsync(string paymentCode);
    }
}
