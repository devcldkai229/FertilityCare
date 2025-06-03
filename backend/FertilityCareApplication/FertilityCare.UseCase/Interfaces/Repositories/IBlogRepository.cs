using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.UseCase.Interfaces.Repositories;

namespace FertilityCare.Infrastructure.Repositories
{
    public interface IBlogRepository : IBaseRepository<Blog, long>
    {
        Task<IEnumerable<Blog>> FindBlogByAuthorAsync(Guid authorId);
    }
}
