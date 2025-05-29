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
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly FertilityCareDBContext _context;

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
    }
}
