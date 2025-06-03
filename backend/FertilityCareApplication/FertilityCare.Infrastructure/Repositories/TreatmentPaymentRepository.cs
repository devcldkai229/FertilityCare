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
    public class TreatmentPaymentRepository : ITreatmentPaymentRepository
    {
        private readonly FertilityCareDBContext _context;

        public TreatmentPaymentRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task DeleteByIdAsync(Guid id)
        {
            var payment = await _context.TreatmentPayments.FindAsync(id);
            if (payment == null)
                throw new NotFoundException($"Payment with ID {id} not found!");

            _context.TreatmentPayments.Remove(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TreatmentPayment>> FindAllAsync()
        {
            return await _context.TreatmentPayments
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<TreatmentPayment> FindByIdAsync(Guid id)
        {
            var payment = await _context.TreatmentPayments.FindAsync(id);
            if (payment == null)
                throw new NotFoundException($"Payment with ID {id} not found!");
            return payment;
        }


        public async Task<TreatmentPayment?> FindByPaymentCodeAsync(string paymentCode)
        {
            return await _context.TreatmentPayments
                .FirstOrDefaultAsync(p => p.PaymentCode == paymentCode);
        }

        public async Task<TreatmentPayment?> FindByTransactionCodeAsync(string transactionCode)
        {
            return await _context.TreatmentPayments
                .FirstOrDefaultAsync(p => p.TransactionCode == transactionCode);
        }

        public async Task<IEnumerable<TreatmentPayment>> FindByTreatmentPlanStepIdAsync(long stepId)
        {
            return await _context.TreatmentPayments
                .Where(p => p.TreatmentPlanStepId == stepId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TreatmentPayment>> FindByUserProfileIdAsync(Guid userId)
        {
            return await _context.TreatmentPayments
                .Where(p => p.UserProfileId == userId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _context.TreatmentPayments.AnyAsync(p => p.Id == id);
        }

        public async Task<TreatmentPayment> SaveAsync(TreatmentPayment entity)
        {
            await _context.TreatmentPayments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TreatmentPayment> UpdateAsync(TreatmentPayment entity)
        {
            var exists = await _context.TreatmentPayments.AnyAsync(p => p.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"Payment with ID {entity.Id} not found!");

            _context.TreatmentPayments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
