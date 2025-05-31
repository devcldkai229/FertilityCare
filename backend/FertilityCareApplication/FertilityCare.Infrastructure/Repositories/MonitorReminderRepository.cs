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
    public class MonitorReminderRepository : IMonitorReminderRepository
    {
        private readonly FertilityCareDBContext _context;

        public MonitorReminderRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<MonitorReminder> CreateAsync(MonitorReminder entity)
        {
            await _context.MonitorReminders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var reminder = await _context.MonitorReminders.FindAsync(id);
            if (reminder == null)
                throw new NotFoundException($"MonitorReminder with Id: {id} not found.");

            _context.MonitorReminders.Remove(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.MonitorReminders.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MonitorReminder>> GetAllAsync()
        {
            return await _context.MonitorReminders.ToListAsync();
        }

        public async Task<MonitorReminder> GetByIdAsync(long id)
        {
            var reminder = await _context.MonitorReminders.FindAsync(id);
            if (reminder == null)
                throw new NotFoundException($"MonitorReminder with Id: {id} not found.");
            return reminder;
        }

        public async Task<IEnumerable<MonitorReminder>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.MonitorReminders.Where(x => x.PatientId == patientId).ToListAsync();
        }

        public async Task<IEnumerable<MonitorReminder>> GetByPlanIdAsync(Guid planId)
        {
            return await _context.MonitorReminders.Where(x => x.ServicePackagePlanId == planId).ToListAsync();
        }

        public async Task<IEnumerable<MonitorReminder>> GetBySenderIdAsync(Guid senderId)
        {
            return await _context.MonitorReminders.Where(x => x.SenderId == senderId).ToListAsync();
        }

        public async Task<IEnumerable<MonitorReminder>> GetDueRemindersAsync(DateTime currentTime)
        {
            return await _context.MonitorReminders
                .Where(x => !x.IsComplete && x.ReminderDate <= currentTime)
                .ToListAsync();
        }

        public async Task<MonitorReminder> UpdateAsync(MonitorReminder entity)
        {
            var exists = await _context.MonitorReminders.AnyAsync(x => x.Id == entity.Id);
            if (!exists)
                throw new NotFoundException($"MonitorReminder with Id: {entity.Id} not found.");

            _context.MonitorReminders.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
