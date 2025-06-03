using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Infrastructure.Data;
using FertilityCare.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FertilityCare.Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private FertilityCareDBContext _context;
        public BlogRepository(FertilityCareDBContext context)
        {
            _context = context;
        }
        public async Task DeleteByIdAsync(long id)
        {
            var loadedBlog = await _context.Blogs.FindAsync(id);
            if (loadedBlog is null)
            {
                throw new NotFoundException($"Blog id:{id} not exist!");
            }
            _context.Blogs.Remove(loadedBlog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Blog>> FindAllAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<IEnumerable<Blog>> FindBlogByAuthorAsync(Guid authorId)
        {
            return await _context.Blogs.Where(x => x.UserProfileId == authorId).ToListAsync();
        }

        public async Task<Blog> FindByIdAsync(long id)
        {
            var loadedBlog = await _context.Blogs.FindAsync(id);
            if(loadedBlog is null)
            {
                throw new NotFoundException($"Blog id:{id} not exist!");
            }
            return loadedBlog;
        }

        public async Task<bool> IsExistAsync(long id)
        {
            var loadedBlog = await _context.Blogs.FindAsync(id);
            if (loadedBlog is null)
            {
                return false;
            }
            return true;
        }

        public async Task<Blog> SaveAsync(Blog entity)
        {
            _context.Blogs.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Blog> UpdateAsync(Blog entity)
        {
            _context.Blogs.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
