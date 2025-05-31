using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
<<<<<<< HEAD
using FertilityCare.Shared.Exceptions;
=======
using FertilityCare.UseCase.Exceptions;
>>>>>>> origin/features/thanghs
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

        public async Task<EggRetrievalCycle> CreateAsync(EggRetrievalCycle entity)
        {
            await _context.EggRetrievalCycles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadEgg = await _context.EggRetrievalCycles.FindAsync(id);
            if (loadEgg is null)
            {
                throw new NotFoundException($"EggRetrievalCycles id:{id} not exits!");
            }
            _context.EggRetrievalCycles.Remove(loadEgg);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadEgg = await _context.EggRetrievalCycles.FindAsync(id);
            if (loadEgg is null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<EggRetrievalCycle>> GetAllAsync()
        {
            return await _context.EggRetrievalCycles.ToListAsync();
        }

        public async Task<IEnumerable<EggRetrievalCycle>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _context.EggRetrievalCycles.Where(x => x.DoctorId.Equals(doctorId)).ToListAsync();
        }

        public async Task<EggRetrievalCycle> GetByIdAsync(Guid id)
        {
            var loadEgg = await _context.EggRetrievalCycles.FindAsync(id);
            if (loadEgg is null)
            {
                throw new NotFoundException($"EggRetrievalCycles id:{id} not exits!");
            }
            return loadEgg;
        }

        public async Task<IEnumerable<EggRetrievalCycle>> GetByPlanIdAsync(Guid planId)
        {
            return await _context.EggRetrievalCycles.Where(x => x.ServicePackagePlanId.Equals(planId)).ToListAsync();
        }
        public async Task<EggRetrievalCycle> UpdateAsync(EggRetrievalCycle entity)
        {
            _context.EggRetrievalCycles.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
