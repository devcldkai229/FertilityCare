using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using FertilityCare.UseCase.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Repositories
{
    public class TreamentStepRepository : ITreatmentStepRepository
    {
        private readonly FertilityCareDBContext _context;

        public TreamentStepRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<TreatmentStep> CreateAsync(TreatmentStep entity)
        {
            await _context.TreatmentSteps.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var loadedStep = await _context.TreatmentSteps.FindAsync(id);   
            if (loadedStep is null)
            {
                throw new NotFoundException($"Treatment Step with Id: {id} does not exist!");
            }

            _context.TreatmentSteps.Remove(loadedStep);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            var loadedStep = await _context.TreatmentSteps.FindAsync(id);
            if (loadedStep is null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<TreatmentStep>> GetAllAsync()
        {
            return await _context.TreatmentSteps.ToListAsync();
        }

        public async Task<TreatmentStep> GetByIdAsync(long id)
        {
            var loadedStep = await _context.TreatmentSteps.FindAsync(id);
            if (loadedStep is null)
            {
                throw new NotFoundException($"Treatment Step with Id: {id} does not exist!");
            }

            return loadedStep;
        }

        public Task<TreatmentStep> UpdateAsync(TreatmentStep entity)
        {
            _context.TreatmentSteps.Update(entity);
            _context.SaveChangesAsync();
            return Task.FromResult(entity);
        }
    }
}
