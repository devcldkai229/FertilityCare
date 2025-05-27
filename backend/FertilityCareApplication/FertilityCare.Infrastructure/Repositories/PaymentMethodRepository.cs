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
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly FertilityCareDBContext _context;

        public PaymentMethodRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task<PaymentMethod> CreateAsync(PaymentMethod entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadPm = await _context.PaymentMethods.FindAsync(id);
            if(loadPm is null)
            {
                throw new NotFoundException($"PaymentMehthod with id: {id} not exist!");
            }
            _context.PaymentMethods.Remove(loadPm);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadPm = await _context.PaymentMethods.FindAsync(id);
            if(loadPm is null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod> GetByIdAsync(Guid id)
        {
            var loadPm = await _context.PaymentMethods.FindAsync(id);
            if(loadPm is null)
            {
                throw new NotFoundException($"PaymentMehthod with id: {id} not exist!");
            }
            return loadPm;
        }

        public async Task<PaymentMethod> UpdateAsync(PaymentMethod entity)
        {
            _context.PaymentMethods.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
