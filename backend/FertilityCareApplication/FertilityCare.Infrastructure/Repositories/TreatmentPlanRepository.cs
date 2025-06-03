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
    public class TreatmentPlanRepository : ITreatmentPlanRepository
    {
        private readonly FertilityCareDBContext _context;

        public TreatmentPlanRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task DeleteByIdAsync(Guid id)
        {
            var plan = await _context.TreatmentPlans.FindAsync(id);
            if (plan == null)
                throw new NotFoundException($"TreatmentPlan with ID {id} not found!");

            _context.TreatmentPlans.Remove(plan);
            await _context.SaveChangesAsync();
        }

        public async Task<TreatmentPlan> FindByIdAsync(Guid id)
        {
            var plan = await _context.TreatmentPlans.FindAsync(id);
            if (plan == null)
                throw new NotFoundException($"TreatmentPlan with ID {id} not found!");
            return plan;
        }


        public async Task<IEnumerable<TreatmentPlan>> FindAllAsync()
        {
            return await _context.TreatmentPlans
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TreatmentPlan>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.TreatmentPlans
                .Where(p => p.PatientId == patientId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TreatmentPlan>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _context.TreatmentPlans
                .Where(p => p.DoctorId == doctorId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        } 

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _context.TreatmentPlans.AnyAsync(p => p.Id == id);
        }

        public async Task<TreatmentPlan> SaveAsync(TreatmentPlan entity)
        {
            await _context.TreatmentPlans.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TreatmentPlan>> SearchByNoteKeywordAsync(string keyword)
        {
            keyword = keyword.ToLower();

            return await _context.TreatmentPlans
                .Where(p => p.Note != null && p.Note.ToLower().Contains(keyword))
                .ToListAsync();
        }

        public async Task<TreatmentPlan> UpdateAsync(TreatmentPlan entity)
        {
            var exists = await _context.TreatmentPlans.AnyAsync(p => p.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"TreatmentPlan with ID {entity.Id} not found!");

            _context.TreatmentPlans.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
