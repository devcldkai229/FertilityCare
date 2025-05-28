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
    public class EmbryoDetailRepository : IEmbryoDetailRepository
    {
        private readonly FertilityCareDBContext _context;
        public EmbryoDetailRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<EmbryoDetail> CreateAsync(EmbryoDetail entity)
        {
            await _context.EmbryoDetails.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loasED = await _context.EmbryoDetails.FindAsync(id);
            if (loasED is null)
            {
                throw new NotFoundException($"EmbryoDetail id:{id} not exists!");
            }
            _context.EmbryoDetails.Remove(loasED);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loasED = await _context.EmbryoDetails.FindAsync(id);
            if (loasED is null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<EmbryoDetail>> GetAllAsync()
        {
            return await _context.EmbryoDetails.ToListAsync();
        }

        public async Task<IEnumerable<EmbryoDetail>> GetByFertilizationIdAsync(Guid fertilizationId)
        {
            return await _context.EmbryoDetails.Where(x => x.EmbryoFertilizationId.Equals(fertilizationId)).ToListAsync();
        }

        public async Task<IEnumerable<EmbryoDetail>> GetByGradeAsync(string grade)
        {
            return await _context.EmbryoDetails.Where(x => x.Grade.Equals(grade)).ToListAsync();
        }

        public async Task<EmbryoDetail> GetByIdAsync(Guid id)
        {
            var loasED = await _context.EmbryoDetails.FindAsync(id);
            if (loasED is null)
            {
                throw new NotFoundException($"EmbryoDetail id:{id} not exists!");
            }
            return loasED;
        }

        public async Task<IEnumerable<EmbryoDetail>> GetByStatusAsync(string status)
        {
            return await _context.EmbryoDetails.Where(x => x.Status.Equals(status)).ToListAsync();
        }

        public async Task<EmbryoDetail> UpdateAsync(EmbryoDetail entity)
        {
            _context.EmbryoDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
