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
    public class PrescriptionItemRepository : IPrescriptionItemRepository
    {
        private readonly FertilityCareDBContext _context;

        public PrescriptionItemRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task DeleteByIdAsync(long id)
        {
            var item = await _context.PrescriptionItems.FindAsync(id);
            if (item == null)
                throw new NotFoundException($"PrescriptionItem with id {id} not found!");

            _context.PrescriptionItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PrescriptionItem>> FindAllAsync()
        {
            return await _context.PrescriptionItems.ToListAsync();
        }

        public async Task<PrescriptionItem?> FindByIdAsync(long id)
        {
            return await _context.PrescriptionItems.FindAsync(id);
        }

        public async Task<PrescriptionItem?> FindItemByIdAsync(long itemId)
        {
            return await FindByIdAsync(itemId); // reuse method
        }

        public async Task<IEnumerable<PrescriptionItem>> FindItemsByPrescriptionIdAsync(Guid prescriptionId)
        {
            return await _context.PrescriptionItems
                .Where(p => p.PrescriptionId == prescriptionId)
                .OrderBy(p => p.MedicationName)
                .ToListAsync();
        }

        public async Task<bool> IsExistAsync(long id)
        {
            return await _context.PrescriptionItems.AnyAsync(p => p.Id == id);
        }

        public async Task<PrescriptionItem> SaveAsync(PrescriptionItem entity)
        {
            await _context.PrescriptionItems.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<PrescriptionItem>> SearchItemsByKeywordAsync(string keyword)
        {
            keyword = keyword.ToLower();

            return await _context.PrescriptionItems
                .Where(p =>
                    p.MedicationName.ToLower().Contains(keyword) ||
                    (p.Dosage != null && p.Dosage.ToLower().Contains(keyword)) ||
                    (p.SpecialInstructions != null && p.SpecialInstructions.ToLower().Contains(keyword)))
                .ToListAsync();
        }

        public async Task<PrescriptionItem> UpdateAsync(PrescriptionItem entity)
        {
            var exists = await _context.PrescriptionItems.AnyAsync(p => p.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"PrescriptionItem with id {entity.Id} not found!");

            _context.PrescriptionItems.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
