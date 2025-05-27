using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IEmbryoDetailRepository : IBaseRepository<EmbryoDetail>
{
    Task<IEnumerable<EmbryoDetail>> GetByFertilizationIdAsync(Guid fertilizationId);
    Task<bool> UpdateStatusAsync(Guid id, string status);
    Task<IEnumerable<EmbryoDetail>> GetViableEmbryosAsync(Guid fertilizationId);
    Task<IEnumerable<EmbryoDetail>> GetByStatusAsync(string status);
    Task<IEnumerable<EmbryoDetail>> GetByGradeAsync(string grade);
    Task<int> GetViableEmbryoCountAsync(Guid fertilizationId);
}
