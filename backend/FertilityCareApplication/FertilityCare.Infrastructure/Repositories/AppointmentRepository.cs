using FertilityCare.Domain.Entities;
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
        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> FindByIdAsync(Guid id)
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
