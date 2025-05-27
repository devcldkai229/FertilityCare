using FertilityCare.Domain.Interfaces.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.UnitOfWorks;

public interface IEmbryologyUnitOfWork
{
    IEggRetrievalCycleRepository _eggRetrievalCycleRepository { get; }

    IEmbryoFertilizationRepository _embryoFertilizationRepository { get; }

    IEmbryoDetailRepository _embryoDetailRepository { get; }

    IFrozenEmbryoStorageRepository _frozenEmbryoStorageRepository { get; }
}
