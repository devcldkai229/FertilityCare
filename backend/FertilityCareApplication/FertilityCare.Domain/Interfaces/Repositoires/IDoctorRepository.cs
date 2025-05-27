using FertilityCare.Domain.Entities;
using FertilityCare.UseCase.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IDoctorRepository : IBaseRepository<Doctor, Guid>    
{
    Task<Doctor> GetByUserProfileIdAsync(Guid userProfileId);
    Task<IEnumerable<Doctor>> GetAllBySpecializationAsync(QueryDoctorSpecification query);
    Task<IEnumerable<Doctor>> GetTopRatedAsync(int count);
    Task<IEnumerable<Doctor>> GetByRatingRangeAsync(decimal minRating, decimal maxRating);
    Task<Doctor> UpdateRatingAsync(Guid doctorId, decimal newRating);
}
