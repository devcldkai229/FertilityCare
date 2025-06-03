using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface ITreatmentServiceRepository : IBaseRepository<TreatmentService, Guid>
    {
        Task<IEnumerable<TreatmentService>> SearchByNameAsync(string keyword);
    }
}
