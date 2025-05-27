using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IEmbryoFertilizationRepository : IBaseRepository<EmbryoFertilization>
{
    Task<IEnumerable<EmbryoFertilization>> GetByCycleIdAsync(Guid cycleId);
    Task<IEnumerable<EmbryoFertilization>> GetByDoctorIdAsync(Guid doctorId);
}
