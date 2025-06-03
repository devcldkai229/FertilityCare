using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IFeedbackRepository : IBaseRepository<Feedback, Guid>
    {
        Task<IEnumerable<Feedback>> FindByPatientIdAsync(Guid patientId);
        Task<IEnumerable<Feedback>> FindByDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<Feedback>> FindByPlanIdAsync(Guid planId);
    }
}
