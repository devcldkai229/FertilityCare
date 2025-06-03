using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Enums;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface ITreatmentPlanRepository : IBaseRepository<TreatmentPlan, Guid>
    {
        Task<IEnumerable<TreatmentPlan>> GetByPatientIdAsync(Guid patientId);
        Task<IEnumerable<TreatmentPlan>> GetByDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<TreatmentPlan>> SearchByNoteKeywordAsync(string keyword);

    }
}
