using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IBlogRepository : IBaseRepository<Blog>
{
    Task<Blog> GetBySlugAsync(string slug);
    Task<IEnumerable<Blog>> GetPublishedAsync();
    Task<IEnumerable<Blog>> GetByAuthorAsync(Guid authorId);
    Task<IEnumerable<Blog>> GetMostViewedAsync(int count);
}
