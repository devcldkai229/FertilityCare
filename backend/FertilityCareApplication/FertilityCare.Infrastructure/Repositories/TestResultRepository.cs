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
    public class TestResultRepository : ITestResultRepository
    {
        private readonly FertilityCareDBContext _context;

        public TestResultRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task DeleteByIdAsync(long id)
        {
            var result = await _context.TestResults.FindAsync(id);
            if (result == null)
                throw new NotFoundException($"TestResult with id {id} not found!");

            _context.TestResults.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<TestResult> FindByIdAsync(long id)
        {
            var result = await _context.TestResults.FindAsync(id);
            if (result == null)
                throw new NotFoundException($"TestResult with id {id} not found!");

            return result;
        }

        public async Task<IEnumerable<TestResult>> FindAllAsync()
        {
            return await _context.TestResults.ToListAsync();
        }

        public async Task<IEnumerable<TestResult>> FindByTreatmentPlanIdAsync(Guid treatmentPlanId)
        {
            return await _context.TestResults
                .Where(r => r.TreatmentPlanId == treatmentPlanId)
                .OrderByDescending(r => r.TestDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestResult>> FindDateRangeAsync(DateTime from, DateTime to)
        {
            return await _context.TestResults
                .Where(r => r.TestDate >= from && r.TestDate <= to)
                .OrderByDescending(r => r.TestDate)
                .ToListAsync();
        }

        public async Task<TestResult?> FindLatestByTreatmentPlanIdAsync(Guid treatmentPlanId)
        {
            return await _context.TestResults
                .Where(r => r.TreatmentPlanId == treatmentPlanId)
                .OrderByDescending(r => r.TestDate)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsExistAsync(long id)
        {
            return await _context.TestResults.AnyAsync(r => r.Id == id);
        }

        public async Task<TestResult> SaveAsync(TestResult entity)
        {
            await _context.TestResults.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TestResult>> SearchByTestNameAsync(string keyword)
        {
            keyword = keyword.ToLower();

            return await _context.TestResults
                .Where(r => r.TestName.ToLower().Contains(keyword))
                .ToListAsync();
        }

        public async Task<TestResult> UpdateAsync(TestResult entity)
        {
            var exists = await _context.TestResults.AnyAsync(r => r.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"TestResult with id {entity.Id} not found!");

            _context.TestResults.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
