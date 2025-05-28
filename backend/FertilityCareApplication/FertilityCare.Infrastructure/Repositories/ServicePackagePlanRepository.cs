using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Enums;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using FertilityCare.UseCase.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FertilityCare.Infrastructure.Repositories
{
    public class ServicePackagePlanRepository : IServicePackagePlanRepository
    {

        private readonly FertilityCareDBContext _context;

        public ServicePackagePlanRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<ServicePackagePlan> CreateAsync(ServicePackagePlan entity)
        {
            await _context.ServicePackagePlans.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.ServicePackagePlans.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("NotFound");
            }

            _context.ServicePackagePlans.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.ServicePackagePlans.AnyAsync(x => x.Id == id);
        }

        public Task<int> GetActivePlansCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ServicePackagePlan>> GetAllAsync()
        {
            return await _context.ServicePackagePlans.ToListAsync();
        }


        public async Task<ServicePackagePlan> GetByIdAsync(Guid id)
        {
            var entity = await _context.ServicePackagePlans.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new NotFoundException("Not Found");
            }
            return entity;
        }


        public async Task<ServicePackagePlan> UpdateAsync(ServicePackagePlan entity)
        {
            
            _context.ServicePackagePlans.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdatePaymentStatusAsync(Guid id, string paymentStatus)
        {
            var entity = await _context.ServicePackagePlans.FindAsync(id);
            if (entity != null && Enum.TryParse(paymentStatus, out PaymentStatus parsedStatus))
            {
                entity.PaymentStatus = parsedStatus;
                entity.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateStatusAsync(Guid id, string status)
        {
            var entity = await _context.ServicePackagePlans.FindAsync(id);
            if (entity != null && Enum.TryParse(status, out SerrvicePlanStatus parsedStatus))
            {
                entity.Status = parsedStatus;
                entity.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
