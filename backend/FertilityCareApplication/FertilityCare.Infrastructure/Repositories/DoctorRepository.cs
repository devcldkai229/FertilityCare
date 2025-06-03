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
    public class DoctorRepository : IDoctorRepository
    {
        private FertilityCareDBContext _context;
        public DoctorRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var loadedDoctor = await _context.Doctors.FindAsync(id);
            if(loadedDoctor is null)
            {
                throw new NotFoundException($"Doctor id:{id} not exist!");
            }
            _context.Doctors.Remove(loadedDoctor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Doctor>> FindAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> FindByIdAsync(Guid id)
        {
            var loadedDoctor = await _context.Doctors.FindAsync(id);
            if(loadedDoctor is null)
            {
                throw new NotFoundException($"Doctor id:{id} not exist!");
            }
            return loadedDoctor;
        }

        public async Task<IEnumerable<Doctor>> FindDoctorByRatingRangeAsync(decimal minRating, decimal maxRating)
        {
            return await _context.Doctors
                .Where(x => x.Rating >= minRating && x.Rating <= maxRating)
                .ToListAsync();
        }

        public async Task<Doctor> FindDoctorByUserProfileAsync(Guid userProfileId)
        {
            var loadedDoctor =  await _context.Doctors
                .FirstOrDefaultAsync(x => x.UserProfileId.Equals(userProfileId));
            if (loadedDoctor is null)
            {
                throw new NotFoundException($"Doctor with UserProfileId: {userProfileId} not exist!");
            }
            return loadedDoctor;
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            var loadedDoctor = await _context.Doctors
                .FindAsync(id);
            if (loadedDoctor is null)
            {
                return false;
            }
            return true;
        }

        public async Task<Doctor> SaveAsync(Doctor entity)
        {
            _context.Doctors.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Doctor> UpdateAsync(Doctor entity)
        {
            _context.Doctors.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Doctor> UpdateRatingAsync(Guid doctorId, decimal newRating)
        {
            var loadedDoctor = await _context.Doctors.FindAsync(doctorId);
            if (loadedDoctor is null)
            {
                throw new NotFoundException($"Doctor with UserProfileId: {doctorId} not exist!");
            }
            loadedDoctor.Rating = newRating;

            _context.Doctors.Update(loadedDoctor);
            await _context.SaveChangesAsync();
            return loadedDoctor;
        }
    }
}
