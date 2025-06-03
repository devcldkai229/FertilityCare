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
    public class PatientRepository : IPatientRepository
    {
        private readonly FertilityCareDBContext _context;

        public PatientRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Patients.CountAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient is null)
            {
                throw new NotFoundException($"Patient with Id: {id} not found.");
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Patient>> FindAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> FindByIdAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient is null)
            {
                throw new NotFoundException($"Patient with Id: {id} not found.");
            }

            return patient;
        }

        public async Task<Patient> FindPatientByUserProfileIdAsync(Guid userProfileId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserProfileId == userProfileId);
            if (patient is null)
            {
                throw new NotFoundException($"Patient with UserProfileId: {userProfileId} not found.");
            }

            return patient;
        }
        public async Task<IEnumerable<Patient>> FindPatientsByMedicalHistoryAsync(string medicalHistory)
        {
            return await _context.Patients
                .Where(p => !string.IsNullOrEmpty(p.MedicalHistory) &&
                            p.MedicalHistory.Contains(medicalHistory, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<IEnumerable<Patient>> FindPatientsByNoteAsync(string note)
        {
            return await _context.Patients
                .Where(p => !string.IsNullOrEmpty(p.Note) &&
                            p.Note.Contains(note, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<IEnumerable<Patient>> FindPatientsByPartnerIdAsync(Guid partnerId)
        {
            return await _context.Patients
                .Where(p => p.PatientParnerId == partnerId)
                .ToListAsync();
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _context.Patients.AnyAsync(p => p.Id == id);
        }

        public async Task<Patient> SaveAsync(Patient entity)
        {
            await _context.Patients.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Patient> UpdateAsync(Patient entity)
        {
            var existing = await _context.Patients.FindAsync(entity.Id);
            if (existing is null)
            {
                throw new NotFoundException($"Patient with Id: {entity.Id} not found.");
            }

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
