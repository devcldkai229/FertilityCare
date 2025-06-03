using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Infrastructure.Data;
using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FertilityCare.Infrastructure.Repositories
{
    public class EmbryoTransferRepository : IEmbryoTransferRepository
    {
        private readonly FertilityCareDBContext _context;
        public EmbryoTransferRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var loadedET = await _context.EmbryoTransfers.FindAsync(id);
            if (loadedET is null)
            {
                throw new NotFoundException($"EmbryoTransfers id:{id} not exist!");
            }
            _context.EmbryoTransfers.Remove(loadedET);
            await _context.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<EmbryoTransfer>> FindAllAsync()
        {
            return await _context.EmbryoTransfers.ToListAsync();
        }

        public async Task<EmbryoTransfer> FindByIdAsync(Guid id)
        {
            var loadedET = await _context.EmbryoTransfers.FindAsync(id);
            if (loadedET is null)
            {
                throw new NotFoundException($"EmbryoTransfers id:{id} not exist!");
            }
            return loadedET;
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            var loadedET = await _context.EmbryoTransfers.FindAsync(id);
            if (loadedET is null)
            {
                return false;
            }
            return true;
        }

        public async Task<EmbryoTransfer> SaveAsync(EmbryoTransfer entity)
        {
            _context.EmbryoTransfers.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EmbryoTransfer> UpdateAsync(EmbryoTransfer entity)
        {
            _context.EmbryoTransfers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
