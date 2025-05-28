using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using FertilityCare.UseCase.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FertilityCare.Infrastructure.Repositories
{
    public class FrozenEmbryoStorageRepository : IFrozenEmbryoStorageRepository
    {
        private readonly FertilityCareDBContext _context;
        public FrozenEmbryoStorageRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<FrozenEmbryoStorage> CreateAsync(FrozenEmbryoStorage entity)
        {
            await _context.FrozenEmbryoStorages.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadFES = await _context.FrozenEmbryoStorages.FindAsync(id);
            if(loadFES is null)
            {
                throw new NotFoundException($"The FrozenEmbryoStorage id:{id} not exists!");
            }
            _context.FrozenEmbryoStorages.Remove(loadFES);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadFES = await _context.FrozenEmbryoStorages.FindAsync(id);
            if (loadFES is null)
            {
                return false;
            }
            return true;
        }
        //do late
        public async Task<IEnumerable<FrozenEmbryoStorage>> GetActiveStorageAsync()
        {
            throw new NotImplementedException();
        }
        //do late
        public Task<int> GetActiveStorageCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FrozenEmbryoStorage>> GetAllAsync()
        {
            return await _context.FrozenEmbryoStorages.ToListAsync();
        }

        public async Task<IEnumerable<FrozenEmbryoStorage>> GetByEmbryoIdAsync(Guid embryoId)
        {
            return await _context.FrozenEmbryoStorages.Where(x => x.EmbryoDetailId.Equals(embryoId)).ToListAsync();
        }

        public async Task<FrozenEmbryoStorage> GetByIdAsync(Guid id)
        {
            var loadFES = await _context.FrozenEmbryoStorages.FindAsync(id);
            if (loadFES is null)
            {
                throw new NotFoundException($"The FrozenEmbryoStorage id:{id} not exists!");
            }
            return loadFES;
        }

        public async Task<IEnumerable<FrozenEmbryoStorage>> GetByTankAsync(string tankName)
        {
            return await _context.FrozenEmbryoStorages.Where(x => x.StorageTank.Equals(tankName)).ToListAsync();
        }
        //do late
        public Task<IEnumerable<FrozenEmbryoStorage>> GetExpiringStorageAsync(DateTime beforeDate)
        {
            throw new NotImplementedException();
        }

        public async Task<FrozenEmbryoStorage> UpdateAsync(FrozenEmbryoStorage entity)
        {
            _context.FrozenEmbryoStorages.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        //do late
        public Task<bool> UpdateStatusAsync(Guid id, string status)
        {
            throw new NotImplementedException();
        }
    }
}
