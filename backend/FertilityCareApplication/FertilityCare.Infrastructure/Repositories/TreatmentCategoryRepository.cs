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
    public class TreatmentCategoryRepository : ITreatmentCategoryRepository
    {
        private readonly FertilityCareDBContext _context;

        public TreatmentCategoryRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task<TreatmentCategory> SaveAsync(TreatmentCategory entity)
        {
            await _context.TreatmentCategories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TreatmentCategory> UpdateAsync(TreatmentCategory entity)
        {
            var exists = await _context.TreatmentCategories.AnyAsync(c => c.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"TreatmentCategory with ID {entity.Id} not found!");

            _context.TreatmentCategories.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var category = await _context.TreatmentCategories.FindAsync(id);
            if (category == null)
                throw new NotFoundException($"TreatmentCategory with ID {id} not found!");

            _context.TreatmentCategories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TreatmentCategory>> FindAllAsync()
        {
            return await _context.TreatmentCategories.ToListAsync();
        }

        public async Task<TreatmentCategory> FindByIdAsync(Guid id)
        {
            var category = await _context.TreatmentCategories.FindAsync(id);
            if (category == null)
                throw new NotFoundException($"TreatmentCategory with ID {id} not found!");
            return category;
        }


        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _context.TreatmentCategories.AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<TreatmentCategory>> SearchByNameAsync(string keyword)
        {
            keyword = keyword.ToLower();
            return await _context.TreatmentCategories
                .Where(c => c.Name.ToLower().Contains(keyword))
                .ToListAsync();
        }
    }
}
