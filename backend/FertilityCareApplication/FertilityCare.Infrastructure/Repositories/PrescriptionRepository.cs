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
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly FertilityCareDBContext _context;

        public PrescriptionRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task DeleteByIdAsync(Guid id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                throw new NotFoundException($"Prescription with ID {id} not found!");

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Prescription>> FindAllAsync()
        {
            return await _context.Prescriptions.ToListAsync();
        }

        public async Task<Prescription> FindByIdAsync(Guid id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                throw new NotFoundException($"Prescription with ID {id} not found!");

            return prescription;
        }

        public async Task<Prescription> FindPrescriptionByIdAsync(Guid prescriptionId)
        {
            return await FindByIdAsync(prescriptionId); // reuse method
        }

        public async Task<IEnumerable<Prescription>> FindPrescriptionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Prescriptions
                .Where(p => p.PrescriptionDate >= startDate && p.PrescriptionDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> FindPrescriptionsByDoctorIdAsync(Guid doctorId)
        {
            return await _context.Prescriptions
                .Where(p => _context.TreatmentPlans
                    .Where(tp => tp.DoctorId == doctorId)
                    .Select(tp => tp.Id)
                    .Contains(p.TreatmentPlanId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> FindPrescriptionsByPatientIdAsync(Guid patientId)
        {
            return await _context.Prescriptions
                .Where(p => _context.TreatmentPlans
                    .Where(tp => tp.PatientId == patientId)
                    .Select(tp => tp.Id)
                    .Contains(p.TreatmentPlanId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> GetByTreatmentPlanIdAsync(Guid treatmentPlanId)
        {
            return await _context.Prescriptions
                .Where(p => p.TreatmentPlanId == treatmentPlanId)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }

        public async Task<Prescription?> GetLatestByTreatmentPlanIdAsync(Guid treatmentPlanId)
        {
            return await _context.Prescriptions
                .Where(p => p.TreatmentPlanId == treatmentPlanId)
                .OrderByDescending(p => p.PrescriptionDate)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _context.Prescriptions.AnyAsync(p => p.Id == id);
        }

        public async Task<Prescription> SaveAsync(Prescription entity)
        {
            await _context.Prescriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Prescription>> SearchPrescriptionsByKeywordAsync(string keyword)
        {
            keyword = keyword.ToLower();
            return await _context.Prescriptions
                .Where(p =>
                    p.Note != null && p.Note.ToLower().Contains(keyword))
                .ToListAsync();
        }

        public async Task<Prescription> UpdateAsync(Prescription entity)
        {
            var exists = await _context.Prescriptions.AnyAsync(p => p.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"Prescription with ID {entity.Id} not found!");

            _context.Prescriptions.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
