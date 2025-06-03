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
    public class EggRetrievalCycleRepository : IEggRetrievalCycleRepository
    {
        private readonly FertilityCareDBContext _context;
        public EggRetrievalCycleRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var loadedER = await _context.EggRetrievalCycles.FindAsync(id);
            if(loadedER is null)
            {
                throw new NotFoundException($"EggRetrievalCycle id:{id} not exist!");
            }
            _context.EggRetrievalCycles.Remove(loadedER);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EggRetrievalCycle>> FindAllAsync()
        {
            return await _context.EggRetrievalCycles.ToListAsync();
        }

        public async Task<EggRetrievalCycle> FindByIdAsync(Guid id)
        {
            var loadedER = await _context.EggRetrievalCycles.FindAsync(id);
            if(loadedER is null)
            {
                throw new NotFoundException($"EggRetrievalCycle id:{id} not exist!");
            }
            return loadedER;
        }
        public async Task<IEnumerable<EggRetrievalCycle>> FindEggRetrievalCycleByPlanIdAsync(Guid planId)
        {
            return await _context.EggRetrievalCycles
                .Where(x => x.TreatmentPlanId.Equals(planId)).ToListAsync();
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            var loadedER = await _context.EggRetrievalCycles.FindAsync(id);
            if (loadedER is null)
            {
                return false;
            }
            return true;
        }

        public async Task<EggRetrievalCycle> SaveAsync(EggRetrievalCycle entity)
        {
            _context.EggRetrievalCycles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EggRetrievalCycle> UpdateAsync(EggRetrievalCycle entity)
        {
            _context.EggRetrievalCycles.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
