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
    public class TreatmentServiceRepository : ITreatmentServiceRepository
    {
        private readonly FertilityCareDBContext _context;

        public TreatmentServiceRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task DeleteByIdAsync(Guid id)
        {
            var service = await _context.TreatmentServices.FindAsync(id);
            if (service == null)
                throw new NotFoundException($"TreatmentService with ID {id} not found!");

            _context.TreatmentServices.Remove(service);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TreatmentService>> FindAllAsync()
        {
            return await _context.TreatmentServices
                .Include(s => s.TreatmentCategory)
                .Include(s => s.TreatmentSteps)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        public async Task<TreatmentService> FindByIdAsync(Guid id)
        {
            var service = await _context.TreatmentServices
                .Include(s => s.TreatmentCategory)
                .Include(s => s.TreatmentSteps)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
                throw new NotFoundException($"TreatmentService with ID {id} not found!");

            return service;
        }


        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _context.TreatmentServices.AnyAsync(s => s.Id == id);
        }

        public async Task<TreatmentService> SaveAsync(TreatmentService entity)
        {
            await _context.TreatmentServices.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TreatmentService>> SearchByNameAsync(string keyword)
        {
            keyword = keyword.ToLower();
            return await _context.TreatmentServices
                .Where(s => s.Name.ToLower().Contains(keyword))
                .Include(s => s.TreatmentCategory)
                .Include(s => s.TreatmentSteps)
                .ToListAsync();
        }

        public async Task<TreatmentService> UpdateAsync(TreatmentService entity)
        {
            var exists = await _context.TreatmentServices.AnyAsync(s => s.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"TreatmentService with ID {entity.Id} not found!");

            _context.TreatmentServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
