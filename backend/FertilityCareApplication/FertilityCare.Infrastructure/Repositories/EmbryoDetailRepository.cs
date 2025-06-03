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
    public class EmbryoDetailRepository : IEmbryoDetailRepository
    {
        private readonly FertilityCareDBContext _context;
        public EmbryoDetailRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var loadedED = await _context.EmbryoDetails.FindAsync(id);
            if (loadedED is null)
            {
                throw new NotFoundException($"EmbryoDetail id:{id} not exists!");
            }
            _context.EmbryoDetails.Remove(loadedED);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmbryoDetail>> FindAllAsync()
        {
            return await _context.EmbryoDetails.ToListAsync();
        }

        public async Task<IEnumerable<EmbryoDetail>> FindByFertilizationIdAsync(Guid fertilizationId)
        {
            return await _context.EmbryoDetails
                .Where(x => x.EmbryoFertilizationId.Equals(fertilizationId)).ToListAsync();
        }

        public async Task<IEnumerable<EmbryoDetail>> FindByGradeAsync(string grade)
        {
            return await _context.EmbryoDetails
                .Where(x => x.Grade.Equals(grade)).ToListAsync();
        }

        public async Task<EmbryoDetail> FindByIdAsync(Guid id)
        {
            var loadedED = await _context.EmbryoDetails.FindAsync(id);
            if (loadedED is null)
            {
                throw new NotFoundException($"EmbryoDetail id:{id} not exists!");
            }
            return loadedED;
        }

        public async Task<IEnumerable<EmbryoDetail>> FindByStatusAsync(bool status)
        {
            return await _context.EmbryoDetails.Where(x => x.IsViable.Equals(status)).ToListAsync();
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            var loadedED = await _context.EmbryoDetails.FindAsync(id);
            if (loadedED is null)
            {
                return false;
            }
            return true;
        }

        public async Task<EmbryoDetail> SaveAsync(EmbryoDetail entity)
        {
            _context.EmbryoDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EmbryoDetail> UpdateAsync(EmbryoDetail entity)
        {
            _context.EmbryoDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
