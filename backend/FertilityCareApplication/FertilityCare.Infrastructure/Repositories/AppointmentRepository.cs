using FertilityCare.Domain.Entities;
using FertilityCare.Infrastructure.Data;
using FertilityCare.UseCase.Interfaces.Repositories;
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
            if (loadAppointment == null) 
            {
                throw new Exception();
            }
            _context.Appointments.Remove(loadAppointment);

        }

        public Task<IEnumerable<Appointment>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> FindAppointmentByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> FindAppointmentByDoctorIdAsync(Guid doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> FindAppointmentByPatientIdAsync(Guid patientId)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> GetTodayAppointmentsAsync(Guid doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> SaveAsync(Appointment entity)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> UpdateAsync(Appointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
