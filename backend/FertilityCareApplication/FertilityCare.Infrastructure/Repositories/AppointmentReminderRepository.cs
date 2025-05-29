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
    public class AppointmentReminderRepository : IAppointmentReminderRepository
    {

        private readonly FertilityCareDBContext _context;

        public AppointmentReminderRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<AppointmentReminder> CreateAsync(AppointmentReminder entity)
        {
            await _context.AppointmentReminders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var loadedReminder = await _context.AppointmentReminders.FindAsync(id);
            if (loadedReminder is null)
            {
                throw new NotFoundException($"Appointment Reminder with Id: {id} does not exist!");
            }

            _context.AppointmentReminders.Remove(loadedReminder);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            var loadedReminder = await _context.AppointmentReminders.FindAsync(id);
            if (loadedReminder is null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<AppointmentReminder>> GetAllAsync()
        {
            return await _context.AppointmentReminders.ToListAsync();
        }

        public async Task<IEnumerable<AppointmentReminder>> GetByAppointmentIdAsync(Guid appointmentId)
        {
            return await _context.AppointmentReminders
                .Where(reminder => reminder.AppointmentId == appointmentId)
                .ToListAsync();
        }

        public async Task<AppointmentReminder> GetByIdAsync(long id)
        {
            var loadedReminder = await _context.AppointmentReminders.FindAsync(id);
            if (loadedReminder is null)
            {
                throw new NotFoundException($"Appointment Reminder with Id: {id} does not exist!");
            }

            return loadedReminder;
        }

        public async Task<IEnumerable<AppointmentReminder>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.AppointmentReminders
                .Where(reminder => reminder.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<bool> MarkAsSentAsync(long id)
        {
            var loadedReminder = await _context.AppointmentReminders.FindAsync(id);
            if (loadedReminder is null)
            {
                throw new NotFoundException($"Appointment Reminder with Id: {id} does not exist!");
            }

            loadedReminder.IsSent = true;
            _context.AppointmentReminders.Update(loadedReminder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AppointmentReminder> UpdateAsync(AppointmentReminder entity)
        {
            _context.AppointmentReminders.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
