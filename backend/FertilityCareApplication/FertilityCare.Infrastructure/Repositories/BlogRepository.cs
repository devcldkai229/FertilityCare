using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using FertilityCare.UseCase.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace FertilityCare.Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly FertilityCareDBContext _context;
        public BlogRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<Blog> CreateAsync(Blog entity)
        {
            await _context.Blogs.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var loadBlog = await _context.Blogs.Where(x => x.Id == id).FirstAsync();
            if(loadBlog is null)
            {
                throw new NotFoundException($"Blog id:{id} not exits!");
            }
            _context.Blogs.Remove(loadBlog);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            var loadBlog = await _context.Blogs.Where(x => x.Id == id).FirstAsync();
            if (loadBlog is null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetByAuthorAsync(Guid authorId)
        {
            return await _context.Blogs.Where(x => x.UserProfileId.Equals(authorId)).ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(long id)
        {
            var loadBlog = await _context.Blogs.FindAsync(id);
            if(loadBlog is null)
            {
                throw new NotFoundException($"Blog id:{id} not exits!");
            }
            return loadBlog;
        }
        public async Task<Blog> UpdateAsync(Blog entity)
        {
             _context.Blogs.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
