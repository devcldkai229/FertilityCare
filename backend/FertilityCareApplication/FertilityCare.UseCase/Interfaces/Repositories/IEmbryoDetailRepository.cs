using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IEmbryoDetailRepository : IBaseRepository<EmbryoDetail, Guid>
    {
        Task<IEnumerable<EmbryoDetail>> FindByFertilizationIdAsync(Guid fertilizationId);
        Task<IEnumerable<EmbryoDetail>> FindByStatusAsync(bool status);
        Task<IEnumerable<EmbryoDetail>> FindByGradeAsync(string grade);
    }
}
