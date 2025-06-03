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
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly FertilityCareDBContext _context;

        public PaymentMethodRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task DeleteByIdAsync(Guid id)
        {
            var method = await _context.PaymentMethods.FindAsync(id);
            if (method == null)
            {
                throw new NotFoundException($"PaymentMethod with id: {id} does not exist!");
            }

            _context.PaymentMethods.Remove(method);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentMethod>> FindActivePaymentMethodsAsync()
        {
            return await _context.PaymentMethods
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<PaymentMethod>> FindAllAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod> FindByIdAsync(Guid id)
        {
            var method = await _context.PaymentMethods.FindAsync(id);
            if (method == null)
            {
                throw new NotFoundException($"PaymentMethod with id: {id} does not exist!");
            }
            return method;
        }

        public async Task<PaymentMethod?> FindPaymentMethodByNameAsync(string name)
        {
            PaymentType paymentType;

            if (int.TryParse(name, out int enumValue))
            {
                paymentType = (PaymentType)enumValue;
            }
            else if (!Enum.TryParse<PaymentType>(name, ignoreCase: true, out paymentType))
            {
                return null;
            }

            return await _context.PaymentMethods
                .FirstOrDefaultAsync(p => p.Name == paymentType);
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _context.PaymentMethods.AnyAsync(p => p.Id == id);
        }

        public async Task<PaymentMethod> SaveAsync(PaymentMethod entity)
        {
            await _context.PaymentMethods.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<PaymentMethod>> SearchByKeywordAsync(string keyword)
        {
            keyword = keyword.ToLower();

            return await _context.PaymentMethods
                .Where(p =>
                    p.Name.ToString().ToLower().Contains(keyword) ||
                    p.Description.ToLower().Contains(keyword))
                .ToListAsync();
        }

        public async Task<PaymentMethod> UpdateAsync(PaymentMethod entity)
        {
            var exists = await _context.PaymentMethods.AnyAsync(p => p.Id == entity.Id);
            if (!exists)
            {
                throw new NotFoundException($"PaymentMethod with id: {entity.Id} does not exist!");
            }

            _context.PaymentMethods.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
