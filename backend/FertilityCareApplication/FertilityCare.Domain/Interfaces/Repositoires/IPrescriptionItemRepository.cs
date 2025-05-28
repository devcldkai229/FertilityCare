using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IPrescriptionItemRepository : IBaseRepository<PrescriptionItem, long>
{
    Task<IEnumerable<PrescriptionItem>> GetByPrescriptionIdAsync(Guid prescriptionId);
}
