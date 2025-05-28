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

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServicePackagePlanStep>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServicePackagePlanStep> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServicePackagePlanStep>> GetByPlanIdAsync(Guid planId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServicePackagePlanStep>> GetCompletedStepsAsync(Guid planId)
        {
            throw new NotImplementedException();
        }

        public Task<ServicePackagePlanStep> GetCurrentStepAsync(Guid planId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServicePackagePlanStep>> GetOverdueStepsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServicePackagePlanStep>> GetPendingStepsAsync(Guid planId)
        {
            throw new NotImplementedException();
        }

        public Task<ServicePackagePlanStep> UpdateAsync(ServicePackagePlanStep entity)
        {
            throw new NotImplementedException();
        }
    }
}
