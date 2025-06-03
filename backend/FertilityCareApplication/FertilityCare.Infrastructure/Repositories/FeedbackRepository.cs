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
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FertilityCareDBContext _context;
        public FeedbackRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var loadedFeedback = await _context.Feedbacks.FindAsync(id);
            if(loadedFeedback is null)
            {
                throw new NotFoundException($"Feedback id:{id} not exist!");
            }
            _context.Feedbacks.Remove(loadedFeedback);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Feedback>> FindAllAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> FindByDoctorIdAsync(Guid doctorId)
        {
            return await _context.Feedbacks
                .Where(x => x.DoctorId.Equals(doctorId)).ToListAsync();
        }

        public async Task<Feedback> FindByIdAsync(Guid id)
        {
            var loadedFeedback = await _context.Feedbacks.FindAsync(id);
            if (loadedFeedback is null)
            {
                throw new NotFoundException($"Feedback id:{id} not exist!");
            }
            return loadedFeedback;
        }

        public async Task<IEnumerable<Feedback>> FindByPatientIdAsync(Guid patientId)
        {
            return await _context.Feedbacks
                .Where(x => x.PatientId.Equals(patientId)).ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> FindByPlanIdAsync(Guid planId)
        {
            return await _context.Feedbacks
                .Where(x => x.TreatmentPlanId.Equals(planId)).ToListAsync();
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            var loadedFeedback = await _context.Feedbacks.FindAsync(id);
            if (loadedFeedback is null)
            {
                return false;
            }
            return true;
        }

        public async Task<Feedback> SaveAsync(Feedback entity)
        {
            _context.Feedbacks.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Feedback> UpdateAsync(Feedback entity)
        {
            _context.Feedbacks.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
