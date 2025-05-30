﻿using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private FertilityCareDBContext _context { get; set; }

        public UserProfileRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> UpdateInfoAsync(UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<UserProfile> CreateInfoAsync(UserProfile profile)
        {
            await _context.UserProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<UserProfile?> FindByUserIdAsync(Guid userId)
        {
            return await _context.UserProfiles.FindAsync(userId);
        }

    }
}
