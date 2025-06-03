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
    public class TreatmentStepRepository : ITreatmentStepRepository
    {
        private readonly FertilityCareDBContext _context;

        public TreatmentStepRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(long id)
        {
            var step = await _context.TreatmentSteps.FindAsync(id);
            if (step == null)
                throw new NotFoundException($"TreatmentStep with ID {id} not found!");

            _context.TreatmentSteps.Remove(step);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TreatmentStep>> FindAllAsync()
        {
            return await _context.TreatmentSteps
                .Include(s => s.TreatmentService)
                .OrderBy(s => s.StepOrder)
                .ToListAsync();
        }

        public async Task<TreatmentStep> FindByIdAsync(long id)
        {
            var step = await _context.TreatmentSteps
                .Include(s => s.TreatmentService)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (step == null)
                throw new NotFoundException($"TreatmentStep with ID {id} not found!");

            return step;
        }

        public async Task<TreatmentStep?> FindStepNameById(long id)
        {
            return await _context.TreatmentSteps
                .Select(s => new TreatmentStep
                {
                    Id = s.Id,
                    StepName = s.StepName
                })
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<TreatmentStep>> FindTreatmentServiceByStepId(long stepId)
        {
            var step = await _context.TreatmentSteps
                .Include(s => s.TreatmentService)
                .FirstOrDefaultAsync(s => s.Id == stepId);

            if (step == null)
                return Enumerable.Empty<TreatmentStep>();

            return await _context.TreatmentSteps
                .Where(s => s.TreatmentServiceId == step.TreatmentServiceId)
                .Include(s => s.TreatmentService)
                .ToListAsync();
        }

        public async Task<bool> IsExistAsync(long id)
        {
            return await _context.TreatmentSteps.AnyAsync(s => s.Id == id);
        }

        public async Task<TreatmentStep> SaveAsync(TreatmentStep entity)
        {
            await _context.TreatmentSteps.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TreatmentStep> UpdateAsync(TreatmentStep entity)
        {
            var exists = await _context.TreatmentSteps.AnyAsync(s => s.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"TreatmentStep with ID {entity.Id} not found!");

            _context.TreatmentSteps.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
