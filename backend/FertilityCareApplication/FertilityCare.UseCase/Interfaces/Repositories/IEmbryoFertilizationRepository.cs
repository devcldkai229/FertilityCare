using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IEmbryoFertilizationRepository : IBaseRepository<EmbryoFertilization, Guid>
    {
        Task<IEnumerable<EmbryoFertilization>> FindByCycleIdAsync(Guid cycleId);
        Task<IEnumerable<EmbryoFertilization>> FindByTreatmentPlanIdAsync(Guid TreatmentPlanId);
    }
}
