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
    public class TreatmentPlanStepRepository : ITreatmentPlanStepRepository
    {
        private readonly FertilityCareDBContext _context;

        public TreatmentPlanStepRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task DeleteByIdAsync(long id)
        {
            var step = await _context.TreatmentPlanSteps.FindAsync(id);
            if (step == null)
                throw new NotFoundException($"TreatmentPlanStep with ID {id} not found!");

            _context.TreatmentPlanSteps.Remove(step);
            await _context.SaveChangesAsync();
        }

        public async Task<TreatmentPlanStep> FindByIdAsync(long id)
        {
            var step = await _context.TreatmentPlanSteps.FindAsync(id);
            if (step == null)
                throw new NotFoundException($"TreatmentPlanStep with ID {id} not found!");
            return step;
        }


        public async Task<IEnumerable<TreatmentPlanStep>> FindAllAsync()
        {
            return await _context.TreatmentPlanSteps
                .OrderByDescending(p => p.StartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TreatmentPlanStep>> FindByStatusAsync(int status)
        {
            var statusEnum = (TreatmentPlanStepStatus)status;

            return await _context.TreatmentPlanSteps
                .Where(p => p.Status == statusEnum)
                .OrderByDescending(p => p.StartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TreatmentPlanStep>> FindByTreatmentPlanIdAsync(Guid treatmentPlanId)
        {
            return await _context.TreatmentPlanSteps
                .Where(p => p.TreatmentPlanId == treatmentPlanId)
                .OrderBy(p => p.StartDate)
                .ToListAsync();
        }

        public async Task<bool> IsExistAsync(long id)
        {
            return await _context.TreatmentPlanSteps.AnyAsync(p => p.Id == id);
        }

        public async Task<TreatmentPlanStep> SaveAsync(TreatmentPlanStep entity)
        {
            await _context.TreatmentPlanSteps.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TreatmentPlanStep>> SearchByNoteAsync(string keyword)
        {
            keyword = keyword.ToLower();

            return await _context.TreatmentPlanSteps
                .Where(p => p.Note != null && p.Note.ToLower().Contains(keyword))
                .ToListAsync();
        }

        public async Task<TreatmentPlanStep> UpdateAsync(TreatmentPlanStep entity)
        {
            var exists = await _context.TreatmentPlanSteps.AnyAsync(p => p.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"TreatmentPlanStep with ID {entity.Id} not found!");

            _context.TreatmentPlanSteps.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
