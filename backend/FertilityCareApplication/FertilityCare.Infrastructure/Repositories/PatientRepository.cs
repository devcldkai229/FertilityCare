using Castle.Core.Logging;
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
    public class PatientRepository : IPatientRepository
    {
        private readonly FertilityCareDBContext _context;

        public PatientRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<Patient> CreateAsync(Patient entity)
        {
            await _context.Patients.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadedPatient = await _context.Patients.FindAsync(id);
            if (loadedPatient is null)
            {
                throw new NotFoundException($"Patient with Id: {id.ToString()} not exist!");
            }

            _context.Patients.Remove(loadedPatient);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadedPatient = await _context.Patients.FindAsync(id);
            if(loadedPatient is null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public Task<IEnumerable<Patient>> GetByBloodTypeAsync(string bloodType)
        {
            throw new NotImplementedException();
        }

        public async Task<Patient> GetByIdAsync(Guid id)
        {
            var loadedPatient = await _context.Patients.FindAsync(id);
            if (loadedPatient is null)
            {
                throw new NotFoundException($"Patient with Id: {id.ToString()} not exist!");
            }

            return loadedPatient;
        }

        public async Task<Patient> GetByPartnerIdAsync(Guid partnerId)
        {
            var loadedPatient = await _context.Patients.FirstOrDefaultAsync(x => x.PatientParnerId == partnerId);
            if (loadedPatient is null)
            {
                throw new NotFoundException($"Patient with partner id: {partnerId} not exist!");
            }

            return loadedPatient;
        }

        public async Task<Patient> GetByUserProfileIdAsync(Guid userProfileId)
        {
            var loadedPatient = await _context.Patients.FirstOrDefaultAsync(x => x.UserProfileId == userProfileId);
            if (loadedPatient is null)
            {
                throw new NotFoundException($"Patient with profile id: {userProfileId} not exist!");
            }

            return loadedPatient;
        }

        public Task<int> GetTotalPatientsCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Patient> UpdateAsync(Patient entity)
        {
            _context.Patients.Update(entity);
            await _context.SaveChangesAsync();
            return entity;      
        }
    }
}
