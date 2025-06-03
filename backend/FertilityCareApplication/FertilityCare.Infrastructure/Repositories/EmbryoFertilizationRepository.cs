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
    public class EmbryoFertilizationRepository : IEmbryoFertilizationRepository
    {
        private readonly FertilityCareDBContext _context;
        public EmbryoFertilizationRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var loadedEF = await _context.EmbryoFertilizations.FindAsync(id);
            if(loadedEF is null)
            {
                throw new NotFoundException($"EmbryoFertilizations id:{id} not exist!");
            }
            _context.EmbryoFertilizations.Remove(loadedEF);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmbryoFertilization>> FindAllAsync()
        {
            return await _context.EmbryoFertilizations.ToListAsync();
        }

        public async Task<IEnumerable<EmbryoFertilization>> FindByCycleIdAsync(Guid cycleId)
        {
            return await _context.EmbryoFertilizations
                .Where(x => x.EggRetrievalCycleId.Equals(cycleId)).ToListAsync();
        }

        public async Task<IEnumerable<EmbryoFertilization>> FindByTreatmentPlanIdAsync(Guid treatmentPlanId)
        {
            return await _context.EmbryoFertilizations
                .Where(x => x.TreatmentPlanId.Equals(treatmentPlanId))
                .ToListAsync();
        }

        public async Task<EmbryoFertilization> FindByIdAsync(Guid id)
        {
            var loadedEF = await _context.EmbryoFertilizations.FindAsync(id);
            if (loadedEF is null)
            {
                throw new NotFoundException($"EmbryoFertilizations id:{id} not exist!");
            }
            return loadedEF;
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            var loadedEF = await _context.EmbryoFertilizations.FindAsync(id);
            if (loadedEF is null)
            {
                return false;
            }
            return true;
        }

        public async Task<EmbryoFertilization> SaveAsync(EmbryoFertilization entity)
        {
            _context.EmbryoFertilizations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EmbryoFertilization> UpdateAsync(EmbryoFertilization entity)
        {
            _context.EmbryoFertilizations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
