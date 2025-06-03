using FertilityCare.Domain.Entities;
using FertilityCare.Infrastructure.Data;
using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.Interfaces.Repositories;
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

        public async Task DeleteByIdAsync(Guid id)
        {
            var loadAppointment = await _context.Appointments.FindAsync(id);
            if (loadAppointment is null) 
            {
                throw new NotFoundException($"Appointment id:{id} not exist!");
            }
            _context.Appointments.Remove(loadAppointment);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Appointment>> FindAllAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> FindAppointmentByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Appointments
                .Where(x => x.AppointmentDate >= startDate && x.AppointmentDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> FindAppointmentByDoctorIdAsync(Guid doctorId)
        {
            return await _context.Appointments.Where(x => x.DoctorId.Equals(doctorId)).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> FindAppointmentByPatientIdAsync(Guid patientId)
        {
            return await _context.Appointments.Where(x => x.PatientId.Equals(patientId)).ToListAsync();
        }

        public async Task<Appointment> FindByIdAsync(Guid id)
        {
            var loadAppointment = await _context.Appointments.FindAsync(id);
            if(loadAppointment is null)
            {
                throw new NotFoundException($"Appointment id:{id} not exist!");
            }
            return loadAppointment;
        }

        public async Task<IEnumerable<Appointment>> GetTodayAppointmentsAsync(Guid doctorId)
        {
            return await _context.Appointments.Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == DateTime.Today).ToListAsync();
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            var loadAppointment = await _context.Appointments.FindAsync(id);
            if (loadAppointment is null)
            {
                return false;
            }
            return true;
        }

        public async Task<Appointment> SaveAsync(Appointment entity)
        {
            _context.Appointments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Appointment> UpdateAsync(Appointment entity)
        {
            _context.Appointments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
