using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IFeedbackRepository : IBaseRepository<Feedback>
{
    Task<IEnumerable<Feedback>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<Feedback>> GetByDoctorIdAsync(Guid doctorId);
    Task<IEnumerable<Feedback>> GetByPlanIdAsync(Guid planId);
    Task<IEnumerable<Feedback>> GetDisplayedFeedbackAsync();
    Task<IEnumerable<Feedback>> GetPendingFeedbackAsync();
    Task<decimal> GetAverageRatingAsync(Guid doctorId);
    Task<IEnumerable<Feedback>> GetTopRatedFeedbackAsync(int count);
}
