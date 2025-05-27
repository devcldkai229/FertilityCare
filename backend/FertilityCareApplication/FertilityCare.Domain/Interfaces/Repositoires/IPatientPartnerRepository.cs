using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IPatientPartnerRepository : IBaseRepository<PatientPartner, Guid>
{
    Task<IEnumerable<PatientPartner>> SearchByNameAsync(string name);
    Task<PatientPartner> GetByEmailAsync(string email);
    Task<PatientPartner> GetByPhoneNumber(string phone);
}
