using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using FertilityCare.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FertilityCare.Infrastructure.Repositories
{
    public class PrescriptionPepository : IPrescriptionRepository
    {
        private readonly FertilityCareDBContext _context;

        public PrescriptionPepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<Prescription> CreateAsync(Prescription entity)
        {
            await _context.Prescriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription is null)
                throw new NotFoundException($"Prescription with Id: {id} does not exist!");

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Prescriptions.AnyAsync(p => p.Id == id);
        }


        public async Task<IEnumerable<Prescription>> GetAllAsync()
        {
            return await _context.Prescriptions.Include(p => p.PrescriptionItems).ToListAsync();
        }

        public async Task<Prescription> GetByIdAsync(Guid id)
        {
            var prescription = await _context.Prescriptions.Include(p => p.PrescriptionItems).FirstOrDefaultAsync(p => p.Id == id);
            if (prescription is null)
                throw new NotFoundException($"Prescription with Id: {id} does not exist!");
            return prescription;
        }

        public async Task<IEnumerable<Prescription>> GetByPlanIdAsync(Guid planId)
        {
            return await _context.Prescriptions
                .Where(p => p.ServicePackagePlanId == planId)
                .Include(p => p.PrescriptionItems)
                .ToListAsync();
        }


        public async Task<IEnumerable<Prescription>> GetExpiringSoonAsync(int days)
        {
            var thresholdDate = DateTime.UtcNow.AddDays(days);
            return await _context.Prescriptions
                .Where(p => p.ExpiryDate.HasValue && p.ExpiryDate <= thresholdDate)
                .Include(p => p.PrescriptionItems)
                .ToListAsync();
        }

        public async Task<Prescription> UpdateAsync(Prescription entity)
        {
            _context.Prescriptions.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
