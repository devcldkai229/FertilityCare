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
    public class PrescriptionItemRepository : IPrescriptionItemRepository
    {
        private readonly FertilityCareDBContext _context;

        public PrescriptionItemRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task<PrescriptionItem> CreateAsync(PrescriptionItem entity)
        {
            await _context.PrescriptionItems.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _context.PrescriptionItems.FindAsync(id);
            if (item is null)
                throw new NotFoundException($"PrescriptionItem with Id: {id} does not exist!");

            _context.PrescriptionItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.PrescriptionItems.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PrescriptionItem>> GetAllAsync()
        {
            return await _context.PrescriptionItems.ToListAsync();
        }

        public async Task<PrescriptionItem> GetByIdAsync(long id)
        {
            var item = await _context.PrescriptionItems.FirstOrDefaultAsync(x => x.Id == id);
            if (item is null)
                throw new NotFoundException($"PrescriptionItem with Id: {id} does not exist!");
            return item;
        }

        public async Task<IEnumerable<PrescriptionItem>> GetByPrescriptionIdAsync(Guid prescriptionId)
        {
            return await _context.PrescriptionItems
                .Where(x => x.PrescriptionId == prescriptionId)
                .ToListAsync();
        }

        public async Task<PrescriptionItem> UpdateAsync(PrescriptionItem entity)
        {
            var exists = await _context.PrescriptionItems.AnyAsync(x => x.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"PrescriptionItem with Id: {entity.Id} does not exist!");

            _context.PrescriptionItems.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
