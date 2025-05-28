using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IFrozenEmbryoStorageRepository : IBaseRepository<FrozenEmbryoStorage, Guid>
{
    Task<IEnumerable<FrozenEmbryoStorage>> GetByEmbryoIdAsync(Guid embryoId);
    Task<bool> UpdateStatusAsync(Guid id, string status);
    Task<IEnumerable<FrozenEmbryoStorage>> GetActiveStorageAsync();
    Task<IEnumerable<FrozenEmbryoStorage>> GetByTankAsync(string tankName);
    Task<IEnumerable<FrozenEmbryoStorage>> GetExpiringStorageAsync(DateTime beforeDate);
    Task<int> GetActiveStorageCountAsync();
}
