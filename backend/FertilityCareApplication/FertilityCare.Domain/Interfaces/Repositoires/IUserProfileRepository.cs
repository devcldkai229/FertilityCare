using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IUserProfileRepository : IBaseRepository<UserProfile>
{
    Task<UserProfile> GetByEmailAsync(string email);
    Task<UserProfile> GetByUsernameAsync(string username);
    Task<IEnumerable<UserProfile>> GetActiveUsersAsync();
    Task<bool> DeactivateAsync(Guid id);
    Task<bool> ActivateAsync(Guid id);
    Task<IEnumerable<UserProfile>> GetByLocationAsync(string city, string province);
}
