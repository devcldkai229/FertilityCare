using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using FertilityCare.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Repositories
{
    public class TreatmentCategoryRepository : ITreatmentCategoryRepository
    {
        private readonly FertilityCareDBContext _context;

        public TreatmentCategoryRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<TreatmentCategory> CreateAsync(TreatmentCategory entity)
        {
            await _context.TreatmentCategories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadedCategory = await _context.TreatmentCategories.FindAsync(id);
            if (loadedCategory is null)
            {
                throw new NotFoundException($"Treatment Category with Id: {id.ToString()} does not exist!");
            }

            _context.TreatmentCategories.Remove(loadedCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadedCategory = await _context.TreatmentCategories.FindAsync(id);
            if (loadedCategory is null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<TreatmentCategory>> GetAllAsync()
        {
            return await _context.TreatmentCategories.ToListAsync();
        }

        public async Task<TreatmentCategory> GetByIdAsync(Guid id)
        {
            var loadedCategory = await _context.TreatmentCategories.FindAsync(id);
            if (loadedCategory is null)
            {
                throw new NotFoundException($"Treatment Category with Id: {id.ToString()} does not exist!");
            }

            return loadedCategory;
        }

        public async Task<TreatmentCategory> UpdateAsync(TreatmentCategory entity)
        {
            _context.TreatmentCategories.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
