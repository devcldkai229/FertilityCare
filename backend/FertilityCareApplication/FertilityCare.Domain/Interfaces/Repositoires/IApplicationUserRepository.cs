using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IApplicationUserRepository<T>
{
    Task<T> FindByUserId(Guid userId);

    Task<T> FindByEmail(string email);

    Task<T> FindByUsername(string username);

    Task<T> CreateUserAsync(T applicationUser, string rawPassword);

}
