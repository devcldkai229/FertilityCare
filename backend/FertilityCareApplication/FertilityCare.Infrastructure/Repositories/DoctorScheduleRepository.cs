using FertilityCare.Domain.DTOs;
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
    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {

        private readonly FertilityCareDBContext _context;

        public DoctorScheduleRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<DoctorSchedule> CreateAsync(DoctorSchedule entity)
        {
            await _context.DoctorSchedules.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var loadedSchedule = await _context.DoctorSchedules.FindAsync(id);
            if (loadedSchedule is null)
            {
                throw new NotFoundException($"Doctor Schedule with Id: {id} does not exist!");
            }

            _context.DoctorSchedules.Remove(loadedSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            var loadedSchedule = await _context.DoctorSchedules.FindAsync(id);
            if (loadedSchedule is null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<DoctorSchedule>> GetAllAsync()
        {
           return await _context.DoctorSchedules.ToListAsync();
        }

        public async Task<IEnumerable<DoctorSchedule>> GetAllBySpecificationAsync(QueryDoctorScheduleSpecification query)
        {
            var queryFilter = _context.DoctorSchedules.AsQueryable();

            if (query.WorkDate.HasValue)
            {
                var workDate = query.WorkDate.Value;
                queryFilter = queryFilter.Where(x => x.WorkDate == workDate);
            }

            if (query.StartTime.HasValue)
            {
                var startTime = query.StartTime.Value;
                queryFilter = queryFilter.Where(x => x.StartTime >= startTime);
            }

            if (query.EndTime.HasValue)
            {
                var endTime = query.EndTime.Value;
                queryFilter = queryFilter.Where(x => x.EndTime <= endTime);
            }

            int skipElement = (query.PageNumber - 1) * query.PageSize;
            return await queryFilter.Skip(skipElement).Take(query.PageSize).ToListAsync();
        }

        public async Task<IEnumerable<DoctorSchedule>> GetAvailableSchedulesAsync()
        {
            return await _context.DoctorSchedules.Where(x => x.IsAvailable == true).ToListAsync();
        }

        public async Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAsync(Guid doctorId)
        {
            var loadedDoctor = await _context.DoctorSchedules.FirstOrDefaultAsync(d => d.DoctorId == doctorId);       
            if (loadedDoctor is null)
            {
                throw new NotFoundException($"Doctor with Id: {doctorId} does not exist!");
            }

            return await _context.DoctorSchedules.Where(d => d.DoctorId == doctorId).ToListAsync();
        }

        public async Task<DoctorSchedule> GetByIdAsync(long id)
        {
            var loadedSchedule = await _context.DoctorSchedules.FindAsync(id);
            if (loadedSchedule is null)
            {
                throw new NotFoundException($"Doctor Schedule with Id: {id} does not exist!");
            }

            return loadedSchedule;
        }

        public async Task<DoctorSchedule> UpdateAsync(DoctorSchedule entity)
        {
            _context.DoctorSchedules.Update(entity);    
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
