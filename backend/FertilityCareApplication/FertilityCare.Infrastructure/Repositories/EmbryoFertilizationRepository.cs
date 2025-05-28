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
    public class EmbryoFertilizationRepository : IEmbryoFertilizationRepository
    {
        private readonly FertilityCareDBContext _content;
        public EmbryoFertilizationRepository(FertilityCareDBContext content)
        {
            _content = content;
        }

        public async Task<EmbryoFertilization> CreateAsync(EmbryoFertilization entity)
        {
            await _content.EmbryoFertilizations.AddAsync(entity);
            await _content.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadEF = await _content.EmbryoFertilizations.FindAsync(id);
            if(loadEF is null)
            {
                throw new NotFoundException($"EmbryoFertilizations id:{id} not exist!");
            }
            _content.EmbryoFertilizations.Remove(loadEF);
            await _content.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadEF = await _content.EmbryoFertilizations.FindAsync(id);
            if (loadEF is null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<EmbryoFertilization>> GetAllAsync()
        {
            return await _content.EmbryoFertilizations.ToListAsync();
        }

        public async Task<IEnumerable<EmbryoFertilization>> GetByCycleIdAsync(Guid cycleId)
        {
            return await _content.EmbryoFertilizations.Where(x => x.EggRetrievalCycleId.Equals(cycleId)).ToListAsync();
        }

        public async Task<IEnumerable<EmbryoFertilization>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _content.EmbryoFertilizations.Where(x => x.DoctorId.Equals(doctorId)).ToListAsync();
        }

        public async Task<EmbryoFertilization> GetByIdAsync(Guid id)
        {
            var loadEF = await _content.EmbryoFertilizations.FindAsync(id);
            if (loadEF is null)
            {
                throw new NotFoundException($"EmbryoFertilizations id:{id} not exist!");
            }
            return loadEF;
        }

        public async Task<EmbryoFertilization> UpdateAsync(EmbryoFertilization entity)
        {
            _content.EmbryoFertilizations.Update(entity);
            await _content.SaveChangesAsync();
            return entity;
        }
    }
}
