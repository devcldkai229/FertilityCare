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
    public class MedicalExaminationRepository : IMedicalExaminationRepository
    {
        private readonly FertilityCareDBContext _context;
        public MedicalExaminationRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(long id)
        {
            var loadedME = await _context.MedicalExaminations.FindAsync(id);
            if(loadedME is null)
            {
                throw new NotFoundException($"MedicalExamination id{id} not exist!");
            }
            _context.MedicalExaminations.Remove(loadedME);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MedicalExamination>> FindAllAsync()
        {
            return await _context.MedicalExaminations.ToListAsync();
        }

        public async Task<IEnumerable<MedicalExamination>> FindByAppointmentId(Guid appointmentId)
        {
            return await _context.MedicalExaminations
                .Where(x => x.AppointmentId.Equals(appointmentId)).ToListAsync();
        }

        public async Task<MedicalExamination> FindByIdAsync(long id)
        {
            var loadedME = await _context.MedicalExaminations.FindAsync(id);
            if (loadedME is null)
            {
                throw new NotFoundException($"MedicalExamination id{id} not exist!");
            }
            return loadedME;
        }

        public async Task<bool> IsExistAsync(long id)
        {
            var loadedME = await _context.MedicalExaminations.FindAsync(id);
            if (loadedME is null)
            {
                return false;
            }
            return true;
        }

        public async Task<MedicalExamination> SaveAsync(MedicalExamination entity)
        {
            _context.MedicalExaminations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MedicalExamination> UpdateAsync(MedicalExamination entity)
        {
            _context.MedicalExaminations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
