using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface ITreatmentServiceRepository : IBaseRepository<TreatmentService, Guid>
{
    Task<IEnumerable<TreatmentService>> GetActiveAsync();
    Task<IEnumerable<TreatmentService>> GetByCategoryIdAsync(Guid categoryId);
    Task<IEnumerable<TreatmentService>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
}
