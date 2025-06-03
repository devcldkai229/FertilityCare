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
    public class AppointmentReminderRepository : IAppointmentReminderRepository
    {
        private readonly FertilityCareDBContext _context;
        public AppointmentReminderRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(long id)
        {
            var loadedReminder = await _context.AppointmentReminders.FindAsync(id);
            if (loadedReminder is null)
            {
                throw new NotFoundException($"AppoinmentRemider id:{id} not exist!");
            }
            _context.AppointmentReminders.Remove(loadedReminder);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AppointmentReminder>> FindAllAsync()
        {
            return await _context.AppointmentReminders.ToListAsync();
        }

        public async Task<IEnumerable<AppointmentReminder>> FindAppointmentReminderByAppointmentIdAsync(Guid appointmentId)
        {
            return await _context.AppointmentReminders
                .Where(x => x.AppointmentId.Equals(appointmentId)).ToListAsync();
        }
        public async Task<AppointmentReminder> FindByIdAsync(long id)
        {
            var loadedReminder = await _context.AppointmentReminders.FindAsync(id);
            if(loadedReminder is null)
            {
                throw new NotFoundException($"AppoinmentRemider id:{id} not exist!");
            }
            return loadedReminder;
        }

        public async Task<bool> IsExistAsync(long id)
        {
            var loadedReminder = await _context.AppointmentReminders.FindAsync(id);
            if (loadedReminder is null)
            {
                return false;
            }
            return true;
        }

        public async Task<AppointmentReminder> SaveAsync(AppointmentReminder entity)
        {
            _context.AppointmentReminders.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AppointmentReminder> UpdateAsync(AppointmentReminder entity)
        {
            _context.AppointmentReminders.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
