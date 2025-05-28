using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IPrescriptionRepository : IBaseRepository<Prescription, Guid>
{
    Task<IEnumerable<Prescription>> GetByPlanIdAsync(Guid planId);
    Task<IEnumerable<Prescription>> GetActiveByPlanIdAsync(Guid planId);
    Task<bool> UpdateStatusAsync(Guid id, string status);
    Task<IEnumerable<Prescription>> GetExpiringSoonAsync(int days);
    Task<IEnumerable<Prescription>> GetByStatusAsync(string status);
}
