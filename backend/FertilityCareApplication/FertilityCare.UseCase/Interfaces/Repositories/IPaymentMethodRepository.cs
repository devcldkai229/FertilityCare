using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod, Guid>
    {
        Task<IEnumerable<PaymentMethod>> FindActivePaymentMethodsAsync();
        Task<PaymentMethod> FindPaymentMethodByNameAsync(string name);
        Task<IEnumerable<PaymentMethod>> SearchByKeywordAsync(string keyword);
    }
}
