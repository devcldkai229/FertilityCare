using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using FertilityCare.UseCase.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Repositories
{
    public class TreatmentServiceRepository : ITreatmentServiceRepository
    {

        private readonly FertilityCareDBContext _context;

        public TreatmentServiceRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<TreatmentService> CreateAsync(TreatmentService entity)
        {
            await _context.TreatmentServices.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadedService = await _context.TreatmentServices.FindAsync(id);
            if (loadedService is null)
            {
                throw new NotFoundException($"Treatment Service with Id: {id.ToString()} does not exist!");
            }

            _context.TreatmentServices.Remove(loadedService);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadedService = await _context.TreatmentServices.FindAsync(id);
            if (loadedService is null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<TreatmentService>> GetActiveAsync()
        {
            return await _context.TreatmentServices.Where(service => service.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<TreatmentService>> GetAllAsync()
        {
            return await _context.TreatmentServices.ToListAsync();
        }

        public async Task<IEnumerable<TreatmentService>> GetByCategoryIdAsync(Guid categoryId)
        {
            return await _context.TreatmentServices.Where(service => service.TreamentCategoryId == categoryId).ToListAsync();
        }

        public async Task<TreatmentService> GetByIdAsync(Guid id)
        {
            var loadedService = await _context.TreatmentServices.FindAsync(id);
            if (loadedService is null)
            {
                throw new NotFoundException($"Treatment Service with Id: {id.ToString()} does not exist!");
            }

            return loadedService;
        }

        public async Task<IEnumerable<TreatmentService>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.TreatmentServices
                .Where(service => service.BasicPrice >= minPrice && service.BasicPrice <= maxPrice)
                .ToListAsync();
        }

        public async Task<TreatmentService> UpdateAsync(TreatmentService entity)
        {
            _context.TreatmentServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
