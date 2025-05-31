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
    public class TestResultRepository : ITestResultRepository
    {
        private readonly FertilityCareDBContext _context;

        public TestResultRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task<TestResult> CreateAsync(TestResult entity)
        {
            await _context.TestResults.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _context.TestResults.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"TestResult with Id: {id} does not exist!");
            }
            _context.TestResults.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.TestResults.AnyAsync(tr => tr.Id == id);
        }

        public async Task<IEnumerable<TestResult>> GetAllAsync()
        {
            return await _context.TestResults.ToListAsync();
        }


        public async Task<IEnumerable<TestResult>> GetByDateRangeAsync(Guid planId, DateTime startDate, DateTime endDate)
        {
            return await _context.TestResults
                .Where(tr => tr.ServicePackagePlanId == planId && tr.TestDate >= startDate && tr.TestDate <= endDate)
                .OrderByDescending(tr => tr.TestDate)
                .ToListAsync();
        }

        public async Task<TestResult> GetByIdAsync(long id)
        {
            var entity = await _context.TestResults.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"TestResult with Id: {id} does not exist!");
            }
            return entity;
        }

        public async Task<IEnumerable<TestResult>> GetByPlanIdAsync(Guid planId)
        {
            return await _context.TestResults
                .Where(tr => tr.ServicePackagePlanId == planId)
                .OrderByDescending(tr => tr.TestDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestResult>> GetLatestResultsAsync(Guid planId, int count)
        {
            return await _context.TestResults
                .Where(tr => tr.ServicePackagePlanId == planId)
                .OrderByDescending(tr => tr.TestDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestResult>> SearchByTestNameAsync(string testName)
        {
            return await _context.TestResults
                .Where(tr => tr.TestName.Contains(testName))
                .OrderByDescending(tr => tr.TestDate)
                .ToListAsync();
        }

        public async Task<TestResult> UpdateAsync(TestResult entity)
        {
            _context.TestResults.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
