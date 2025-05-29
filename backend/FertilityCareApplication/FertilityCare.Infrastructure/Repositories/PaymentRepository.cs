using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using FertilityCare.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FertilityCare.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly FertilityCareDBContext _context;

        public PaymentRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreateAsync(Payment entity)
        {
            await _context.Payments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadP = await _context.Payments.FindAsync(id);
            if (loadP is null)
            {
                throw new NotFoundException($"Payment id:{id} not exist!");
            }
            _context.Payments.Remove(loadP);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadP = await _context.Payments.FindAsync(id);
            if(loadP is null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> GetByIdAsync(Guid id)
        {
            var loadP = await _context.Payments.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(loadP is null)
            {
                throw new NotFoundException($"Payment id:{id} not exist!");
            }
            return loadP;
        }

        public async Task<Payment> GetByPaymentCodeAsync(string paymentCode)
        {
            return await _context.Payments.Where(x => x.PaymentCode.Equals(paymentCode)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Payment>> GetByPlanIdAsync(Guid planId)
        {
            return await _context.Payments.Where(x => x.ServicePackagePlanId == planId).ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetByStatusAsync(string status)
        {
            return await _context.Payments.Where(x => x.Status.Equals(status)).ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Payments.Where(x => x.UserProfileId == userId).ToListAsync();
        }

        public async Task<Payment> UpdateAsync(Payment entity)
        {
            _context.Payments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        //để sau
        public Task<bool> UpdateStatusAsync(Guid id, string status)
        {
            throw new NotImplementedException();
        }
    }
}
