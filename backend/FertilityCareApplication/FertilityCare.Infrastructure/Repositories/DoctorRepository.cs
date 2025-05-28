using FertilityCare.Domain.Constant;
using FertilityCare.Domain.DTOs;
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
    public class DoctorRepository : IDoctorRepository
    {

        private readonly FertilityCareDBContext _context;

        public DoctorRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<Doctor> CreateAsync(Doctor entity)
        {
            await _context.Doctors.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadedDoctor = await _context.Doctors.FindAsync(id);
            if (loadedDoctor is null)
            {
                throw new NotFoundException($"Patient with Id: {id.ToString()} not exist!");
            }

            _context.Doctors.Remove(loadedDoctor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadedDoctor = await _context.Doctors.FindAsync(id);
            if(loadedDoctor is null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Doctor>> GetAllBySpecializationAsync(QueryDoctorSpecification query)
        {
            var queryFilter= _context.Doctors.Where(x => x.IsAcceptingPatients == true).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.Specialization))
            {
                queryFilter = queryFilter.Where(x => x.Specialization == query.Specialization);
            }

            if(query.YearsOfExperience.HasValue)
            {
                queryFilter = queryFilter.Where(x => x.YearsOfExperience >= query.YearsOfExperience);
            }

            if(query.MinRating.HasValue)
            {
                queryFilter = queryFilter.Where(x => x.Rating >= query.MinRating);
            }

            if(query.MinPatientsServed.HasValue)
            {
                queryFilter = queryFilter.Where(x => x.PatientsServed >= query.MinPatientsServed);
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                queryFilter = query.SortBy switch
                {
                    ApplicationConstant.SortByRating => query.IsDescending 
                        ? queryFilter.OrderByDescending(x => x.Rating) 
                        : queryFilter.OrderBy(x => x.Rating),

                    ApplicationConstant.SortByPatientsServed => query.IsDescending 
                        ? queryFilter.OrderByDescending(x => x.PatientsServed) 
                        : queryFilter.OrderBy(x => x.PatientsServed),
                     _ => queryFilter
                };

            }

            int skipElement = (query.PageNumber - 1) * query.PageSize;
            return await queryFilter.Skip(skipElement).Take(query.PageSize).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(Guid id)
        {
            var loadedDoctor = await _context.Doctors.FindAsync(id);
            if (loadedDoctor is null)
            {
                throw new NotFoundException($"Doctor with Id: {id.ToString()} not exist!");
            }

            return loadedDoctor;
        }

        public async Task<IEnumerable<Doctor>> GetByRatingRangeAsync(decimal minRating, decimal maxRating)
        {
            return await _context.Doctors.Where(d => d.Rating >= minRating && d.Rating <= maxRating)
                .ToListAsync();
        }

        public async Task<Doctor> GetByUserProfileIdAsync(Guid userProfileId)
        {
            var loadedDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserProfileId == userProfileId);
            if (loadedDoctor is null)
            {
                throw new NotFoundException($"Doctor with UserProfileId: {userProfileId} not exist!");
            }

            return loadedDoctor;
        }

        public async Task<IEnumerable<Doctor>> GetTopRatedAsync(int count)
        {
            return await _context.Doctors.OrderByDescending(d => d.Rating).Take(count).ToListAsync();
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
            if(loadedDoctor is null)
            {
                throw new NotFoundException($"Doctor with Id: {doctorId.ToString()} not exist!");
            }

            loadedDoctor.Rating = newRating;

            _context.Doctors.Update(loadedDoctor);
            await _context.SaveChangesAsync();
            return loadedDoctor;
        }

    }
}
