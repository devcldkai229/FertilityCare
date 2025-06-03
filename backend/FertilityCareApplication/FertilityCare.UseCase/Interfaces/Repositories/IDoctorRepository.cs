using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;

namespace FertilityCare.UseCase.Interfaces.Repositories
{
    public interface IDoctorRepository : IBaseRepository<Doctor, Guid>
    {
        Task<IEnumerable<Doctor>> FindDoctorByRatingRangeAsync(decimal minRating, decimal maxRating);
        Task<Doctor> FindDoctorByUserProfileAsync(Guid userProfileId);
        Task<Doctor> UpdateRatingAsync(Guid doctorId, decimal newRating);
    }
}
