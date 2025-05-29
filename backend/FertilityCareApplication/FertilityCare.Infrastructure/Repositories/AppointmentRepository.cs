using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Domain.Enums;
using FertilityCare.Infrastructure.Data;
using FertilityCare.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly FertilityCareDBContext _context;

        private IDbContextTransaction _currentTransaction;

        public AppointmentRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CancelAppointmentAsync(Guid id, string reason)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if(appointment is null)
            {
                throw new NotFoundException($"Appointment with Id: {id} does not exist.");
            }


            return appointment;
        }

        public async Task<Appointment> CreateAsync(Appointment entity)
        {
            _context.Appointments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadedAppointment = await _context.Appointments.FindAsync(id);
            if (loadedAppointment is null)
            {
                throw new NotFoundException($"Appointment with Id: {id} does not exist!");
            }

            _context.Appointments.Remove(loadedAppointment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadedAppointment = await _context.Appointments.FindAsync(id);
            if (loadedAppointment is null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Appointments
                .Where(a => a.AppointmentDate >= startDate && a.AppointmentDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<Appointment> GetByIdAsync(Guid id)
        {
            var loadedAppointment = await _context.Appointments.FindAsync(id);
            if (loadedAppointment is null)
            {
                throw new NotFoundException($"Appointment with Id: {id} does not exist!");
            }

            return loadedAppointment;
        }

        public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByTreatmentServiceAsync(Guid treatmentServiceId)
        {
            return await _context.Appointments
                .Where(a => a.TreatmentServiceId == treatmentServiceId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetTodayAppointmentsAsync(Guid doctorId)
        {
            return await _context.Appointments.Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == DateTime.Today).ToListAsync();
        }

        public async Task<Appointment> UpdateAsync(Appointment entity)
        {
            _context.Appointments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsyncBySchedule(long DoctorScheduleId)
        {
             var a = await _context.Appointments.CountAsync(x => x.DoctorScheduleId == DoctorScheduleId 
             && x.Status != AppointmentStatus.Cancelled 
             && x.Status != AppointmentStatus.Completed);
            return a;
        }

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if(_currentTransaction is not null)
            {
                throw new InvalidOperationException("Transaction is in use!");
            }

            _currentTransaction = await _context.Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public virtual async Task CommitTransactionAsync()
        {
            if(_currentTransaction is null)
            {
                throw new InvalidOperationException("Not found transaction to commit!");
            }

            try
            {
                await _currentTransaction.CommitAsync();
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        public virtual async Task RollbackTransactionAsync()
        {
            if(_currentTransaction is null)
            {
                throw new InvalidOperationException("Not found transaction to commit!");
            }

            try
            {
                await _currentTransaction.RollbackAsync();
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        public virtual void Dispose()
        {
            _currentTransaction?.Dispose();
            _context?.Dispose();
        }

      
    }
}
