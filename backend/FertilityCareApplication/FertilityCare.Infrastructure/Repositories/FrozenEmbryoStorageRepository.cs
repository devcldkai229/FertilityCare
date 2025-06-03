using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Enums;
using FertilityCare.Infrastructure.Data;
using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.Interfaces.Repositories;
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

        public async Task DeleteByIdAsync(Guid id)
        {
            var loadedFES = await _context.FrozenEmbryoStorages.FindAsync(id);
            if(loadedFES is null)
            {
                throw new NotFoundException($"FrozenEmbryoStorage id:{id} not exist!");
            }
            _context.FrozenEmbryoStorages.Remove( loadedFES );
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FrozenEmbryoStorage>> FindAllAsync()
        {
            return await _context.FrozenEmbryoStorages.ToListAsync();
        }

        public async Task<IEnumerable<FrozenEmbryoStorage>> FindByEmbryoIdDetailAsync(Guid embryoId)
        {
            return await _context.FrozenEmbryoStorages
                .Where(x => x.EmbryoDetailId.Equals(embryoId)).ToListAsync();
        }

        public async Task<FrozenEmbryoStorage> FindByIdAsync(Guid id)
        {
            var loadedFES = await _context.FrozenEmbryoStorages.FindAsync(id);
            if (loadedFES is null)
            {
                throw new NotFoundException($"FrozenEmbryoStorage id:{id} not exist!");
            }
            return loadedFES;
        }

        public async Task<IEnumerable<FrozenEmbryoStorage>> FindByTankAsync(string tankName)
        {
            return await _context.FrozenEmbryoStorages
                .Where(x => x.StorageTank.Equals(tankName)).ToListAsync();
        }

        public async Task<IEnumerable<FrozenEmbryoStorage>> FindByTreatmentPlanId(Guid treatmentPlanId)
        {
            return await _context.FrozenEmbryoStorages
                .Where(x => x.TreatmentPlanId.Equals(treatmentPlanId)).ToListAsync();
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            var loadedFES = await _context.FrozenEmbryoStorages.FindAsync(id);
            if (loadedFES is null)
            {
                return false;
            }
            return true;
        }

        public async Task<FrozenEmbryoStorage> SaveAsync(FrozenEmbryoStorage entity)
        {
            _context.FrozenEmbryoStorages.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<FrozenEmbryoStorage>> TakeActiveStorageAsync()
        {
            return await _context.FrozenEmbryoStorages
                .Where(x => x.Status.Equals(StorageStatus.Active)).ToListAsync();
        }

        public async Task<FrozenEmbryoStorage> UpdateAsync(FrozenEmbryoStorage entity)
        {
            _context.FrozenEmbryoStorages.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateStatusAsync(Guid id, StorageStatus status)
        {
            var loadedFES = await _context.FrozenEmbryoStorages.FindAsync(id);
            if (loadedFES is null)
            {
                return false;
            }
            loadedFES.Status = status;
            _context.FrozenEmbryoStorages.Update(loadedFES);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
