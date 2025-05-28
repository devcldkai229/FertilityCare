using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Repositories
{
    public class EmbryoTransferRepository : IEmbryoTransferRepository
    {
        public Task<EmbryoTransfer> CreateAsync(EmbryoTransfer entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmbryoTransfer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmbryoTransfer> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EmbryoTransfer> UpdateAsync(EmbryoTransfer entity)
        {
            throw new NotImplementedException();
        }
    }
}
