using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Enums;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Identity;
using FertilityCare.UseCase.DTOs.Doctors;
using FertilityCare.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Services
{
    public class DoctorService : IDoctorService
    {

        private readonly IApplicationUserRepository<ApplicationUser> _applicationUserRepository;

        private readonly IUserProfileRepository _userProfileRepository;

        private readonly IDoctorRepository _doctorRepository;

        private readonly IDoctorScheduleRepository _doctorScheduleRepository;

        public DoctorService(IApplicationUserRepository<ApplicationUser> applicationUserRepository,
            IDoctorRepository doctorRepository,
            IDoctorScheduleRepository doctorScheduleRepository,
            IUserProfileRepository userProfileRepository)
        {
            _applicationUserRepository = applicationUserRepository;
            _doctorRepository = doctorRepository;
            _doctorScheduleRepository = doctorScheduleRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<DoctorDTO> CreateAsync(CreateDoctorRequestDTO request)
        {
            var applicationUser = await _applicationUserRepository.CreateUserAsync(new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                PasswordHash = request.Password,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = false
            });

            var doctorProfile = await _userProfileRepository.CreateInfoAsync(new UserProfile
            {
                Id = applicationUser.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Gender = request.Gender == Gender.Male.ToString() ? Gender.Male 
                : request.Gender == Gender.Female.ToString() ? Gender.Female : Gender.Unknown,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
            });

            var loadedDoctor = await _doctorRepository.CreateAsync(new Doctor
            {
                UserProfile = doctorProfile,
                UserProfileId = doctorProfile.Id,
                Degree = request.Degree,
                Specialization = request.Specialization
            });

            return 
        }

        public Task<DoctorDTO> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DoctorDTO> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
