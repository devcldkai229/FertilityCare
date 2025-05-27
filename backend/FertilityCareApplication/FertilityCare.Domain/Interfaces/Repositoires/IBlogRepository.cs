using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IBlogRepository : IBaseRepository<Blog, long>
{
    Task<IEnumerable<Blog>> GetByAuthorAsync(Guid authorId);

}
