using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

<<<<<<< HEAD
public interface IUserProfileRepository 
=======
public interface IUserProfileRepository : IBaseRepository<UserProfile, Guid>
>>>>>>> origin/features/thanghs
{
    Task<UserProfile> UpdateInfoAsync(UserProfile profile);

    Task<UserProfile> CreateInfoAsync(UserProfile profile);

    Task<UserProfile?> FindByUserIdAsync(Guid userId);
}
