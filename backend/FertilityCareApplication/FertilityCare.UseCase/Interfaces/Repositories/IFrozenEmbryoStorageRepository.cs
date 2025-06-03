using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Enums;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IFrozenEmbryoStorageRepository : IBaseRepository<FrozenEmbryoStorage, Guid>
    {
        Task<IEnumerable<FrozenEmbryoStorage>> FindByEmbryoIdDetailAsync(Guid embryoId);
        Task<IEnumerable<FrozenEmbryoStorage>> FindByTreatmentPlanId(Guid treatmentPlanId);
        Task<bool> UpdateStatusAsync(Guid id, StorageStatus status);
        Task<IEnumerable<FrozenEmbryoStorage>> TakeActiveStorageAsync();
        Task<IEnumerable<FrozenEmbryoStorage>> FindByTankAsync(string tankName);
    }
}
