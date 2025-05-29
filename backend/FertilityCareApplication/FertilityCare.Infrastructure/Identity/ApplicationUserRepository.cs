using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using FertilityCare.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Identity
{
    public class ApplicationUserRepository : IApplicationUserRepository<ApplicationUser>
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> FindByUserId(Guid userId)
        {
            var result = await _userManager.FindByIdAsync(userId.ToString());
            if(result is null)
            {
                throw new UserNotExistException($"User with id: {userId.ToString()} not found!");
            }

            return result;
        } 

        public async Task<ApplicationUser> FindByEmail(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result is null)
            {
                throw new UserNotExistException($"User with email: {email} not found!");
            }

            return result;
        }

        public async Task<ApplicationUser> FindByUsername(string username)
        {
            var result = await _userManager.FindByNameAsync(username);
            if (result is null)
            {
                throw new UserNotExistException($"User with username: {username} not found!");
            }

            return result;
        }

        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser applicationUser, string rawPassword)
        {
            var result = await _userManager.CreateAsync(applicationUser, rawPassword);
            if (!result.Succeeded)
            {
                throw new CreateUserFailedExpception($"Create new user failed!");
            }
            return applicationUser;
        }


    }
}
