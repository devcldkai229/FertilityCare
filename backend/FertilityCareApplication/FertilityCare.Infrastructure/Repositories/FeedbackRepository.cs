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
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FertilityCareDBContext _context;
        public FeedbackRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task<Feedback> CreateAsync(Feedback entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadFeedback = await _context.Feedbacks.FindAsync(id);
            if (loadFeedback is null)
            {
                throw new NotFoundException($"Feedback id:{id} not exist!");
            }
            _context.Feedbacks.Remove(loadFeedback);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadFeedback = await _context.Feedbacks.FindAsync(id);
            if(loadFeedback is null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _context.Feedbacks.Where(x => x.DoctorId.Equals(doctorId)).ToListAsync();
        }

        public async Task<Feedback> GetByIdAsync(Guid id)
        {
            var loadFeedback = await _context.Feedbacks.FindAsync(id);
            if(loadFeedback is null)
            {
                throw new NotFoundException($"Feedback id:{id} not exist!");
            }
            return loadFeedback;
        }

        public async Task<IEnumerable<Feedback>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.Feedbacks.Where(x => x.UserProfileId.Equals(patientId)).ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetByPlanIdAsync(Guid planId)
        {
            return await _context.Feedbacks.Where(x => x.ServicePackagePlanId.Equals(planId)).ToListAsync();
        }
        public async Task<Feedback> UpdateAsync(Feedback entity)
        {
            _context.Feedbacks.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
