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
    internal class TreatmentProgressReportRepository : ITreatmentProgressReportRepository
    {
        private readonly FertilityCareDBContext _context;

        public TreatmentProgressReportRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task<TreatmentProgressReport> CreateAsync(TreatmentProgressReport entity)
        {
            await _context.TreatmentProgressReports.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var report = await _context.TreatmentProgressReports.FindAsync(id);
            if (report is null)
                throw new NotFoundException($"TreatmentProgressReport with Id: {id} does not exist!");

            _context.TreatmentProgressReports.Remove(report);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.TreatmentProgressReports.AnyAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<TreatmentProgressReport>> GetAllAsync()
        {
            return await _context.TreatmentProgressReports.ToListAsync();
        }

        public async Task<IEnumerable<TreatmentProgressReport>> GetByDateRangeAsync(Guid planId, DateTime startDate, DateTime endDate)
        {
            return await _context.TreatmentProgressReports
                .Where(r => r.ServicePackagePlanId == planId && r.ReportDate >= startDate && r.ReportDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TreatmentProgressReport>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _context.TreatmentProgressReports
                .Where(r => r.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<TreatmentProgressReport> GetByIdAsync(Guid id)
        {
            var report = await _context.TreatmentProgressReports.FindAsync(id);
            if (report is null)
                throw new NotFoundException($"TreatmentProgressReport with Id: {id} does not exist!");

            return report;
        }

        public async Task<IEnumerable<TreatmentProgressReport>> GetByPlanIdAsync(Guid planId)
        {
            return await _context.TreatmentProgressReports
                .Where(r => r.ServicePackagePlanId == planId)
                .ToListAsync();
        }

        public async Task<TreatmentProgressReport> GetLatestReportAsync(Guid planId)
        {
            return await _context.TreatmentProgressReports
                .Where(r => r.ServicePackagePlanId == planId)
                .OrderByDescending(r => r.ReportDate)
                .FirstOrDefaultAsync();
        }

        public async Task<TreatmentProgressReport> UpdateAsync(TreatmentProgressReport entity)
        {
            _context.TreatmentProgressReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
