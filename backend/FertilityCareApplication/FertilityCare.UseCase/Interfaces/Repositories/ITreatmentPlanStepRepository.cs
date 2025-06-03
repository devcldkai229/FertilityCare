using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface ITreatmentPlanStepRepository : IBaseRepository<TreatmentPlanStep, long>
    {
        Task<IEnumerable<TreatmentPlanStep>> FindByTreatmentPlanIdAsync(Guid treatmentPlanId);
        Task<IEnumerable<TreatmentPlanStep>> FindByStatusAsync(int status);
        Task<IEnumerable<TreatmentPlanStep>> SearchByNoteAsync(string keyword);
    }
}
