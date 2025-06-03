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
    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {
        private readonly FertilityCareDBContext _context;
        public DoctorScheduleRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(long id)
        {
            var loadedDS = await _context.DoctorSchedules.FindAsync(id);
            if(loadedDS is null)
            {
                throw new NotFoundException($"Doctor Schedules id:{id} not exist!");
            }
            _context.DoctorSchedules.Remove(loadedDS);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorSchedule>> FindAllAsync()
        {
            return await _context.DoctorSchedules.ToListAsync();
        }

        public async Task<IEnumerable<DoctorSchedule>> FindAllDoctorScheduleByAvailableSchedulesAsync()
        {
            return await _context.DoctorSchedules
                .Where(x => x.IsAcceptingPatients == true)
                .ToListAsync();
        }

        public async Task<DoctorSchedule> FindByIdAsync(long id)
        {
            var loadedDS = await _context.DoctorSchedules.FindAsync(id);
            if (loadedDS is null)
            {
                throw new NotFoundException($"Doctor Schedules id:{id} not exist!");
            }
            return loadedDS;
        }

        public async Task<IEnumerable<DoctorSchedule>> FindDoctorScheduleByDoctorIdAsync(Guid doctorId)
        {
            return await _context.DoctorSchedules
                .Where(x => x.DoctorId.Equals(doctorId)).ToListAsync();
        }

        public async Task<bool> IsExistAsync(long id)
        {
            var loadedDS = await _context.DoctorSchedules.FindAsync(id);
            if (loadedDS is null)
            {
                return false;
            }
            return true;
        }

        public async Task<DoctorSchedule> SaveAsync(DoctorSchedule entity)
        {
            _context.DoctorSchedules.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DoctorSchedule> UpdateAsync(DoctorSchedule entity)
        {
            _context.DoctorSchedules.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
