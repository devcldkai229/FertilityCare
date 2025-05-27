using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IPatientRepository : IBaseRepository<Patient, Guid>
{
    Task<Patient> GetByUserProfileIdAsync(Guid userProfileId);
    Task<Patient> GetByPartnerIdAsync(Guid partnerId);
    Task<IEnumerable<Patient>> GetByBloodTypeAsync(string bloodType);
    Task<int> GetTotalPatientsCountAsync();

}
