using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IEggRetrievalCycleRepository : IBaseRepository<EggRetrievalCycle, Guid>
{
    Task<IEnumerable<EggRetrievalCycle>> GetByPlanIdAsync(Guid planId);
    Task<EggRetrievalCycle> GetLatestCycleAsync(Guid planId);
    Task<IEnumerable<EggRetrievalCycle>> GetByDoctorIdAsync(Guid doctorId);
    Task<int> GetTotalEggsRetrievedAsync(Guid planId);
    Task<decimal> GetAverageEggsPerCycleAsync();
}
