using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IPatientPartnerRepository : IBaseRepository<PatientPartner, Guid>
    {
        Task<IEnumerable<PatientPartner>> FindPartnersByPatientIdAsync(Guid patientId);
        Task<PatientPartner> FindPartnerByEmailAsync(string email);
        Task<PatientPartner> FindPartnerByPhoneNumberAsync(string phoneNumber);
        Task<IEnumerable<PatientPartner>> SearchByEmailKeywordAsync(string keyword);
        Task<IEnumerable<PatientPartner>> SearchByPhoneKeywordAsync(string keyword);
    }
}
