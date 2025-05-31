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
    public class ServicePackagePlanStepRepository : IServicePackagePlanStepRepository

    {
        private readonly FertilityCareDBContext _context;

        public ServicePackagePlanStepRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<ServicePackagePlanStep> CreateAsync(ServicePackagePlanStep entity)
        {
            await _context.ServicePackagePlanSteps.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var step = await _context.ServicePackagePlanSteps.FindAsync(id);
            if (step is null)
            {
                throw new NotFoundException($"Step with Id: {id} does not exist!");
            }

            _context.ServicePackagePlanSteps.Remove(step);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.ServicePackagePlanSteps.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ServicePackagePlanStep>> GetAllAsync()
        {
            return await _context.ServicePackagePlanSteps.ToListAsync();
        }

        public async Task<ServicePackagePlanStep> GetByIdAsync(long id)
        {
            var step = await _context.ServicePackagePlanSteps.FindAsync(id);
            if (step is null)
            {
                throw new NotFoundException($"Step with Id: {id} does not exist!");
            }

            return step;
        }

        public async Task<IEnumerable<ServicePackagePlanStep>> GetByPlanIdAsync(Guid planId)
        {
            return await _context.ServicePackagePlanSteps
                .Where(s => s.ServicePackagePlanId == planId) 
                .ToListAsync();
        }

        public async Task<ServicePackagePlanStep> GetCurrentStepAsync(Guid planId)
        {
            return await _context.ServicePackagePlanSteps
                .Where(s => s.ServicePackagePlanId == planId && s.IsComplete == false)
                .FirstOrDefaultAsync();
        }

        public async Task<ServicePackagePlanStep> UpdateAsync(ServicePackagePlanStep entity)
        {
            _context.ServicePackagePlanSteps.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
