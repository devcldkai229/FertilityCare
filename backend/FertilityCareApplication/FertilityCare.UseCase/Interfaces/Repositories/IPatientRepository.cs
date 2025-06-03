using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IPatientRepository : IBaseRepository<Patient, Guid>
    {
        Task<Patient> FindPatientByUserProfileIdAsync(Guid userProfileId);
        Task<IEnumerable<Patient>> FindPatientsByPartnerIdAsync(Guid partnerId);
        Task<IEnumerable<Patient>> FindPatientsByMedicalHistoryAsync(string medicalHistory);
        Task<IEnumerable<Patient>> FindPatientsByNoteAsync(string note);
        Task<int> CountAsync();

    }
}
