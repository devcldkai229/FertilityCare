using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Enums;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Identity;
using FertilityCare.UseCase.DTOs.Doctors;
using FertilityCare.UseCase.Interfaces;
using FertilityCare.UseCase.Mappers;
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
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = false
            }, request.Password);

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

            return loadedDoctor.MapToDoctorDTO();
        }

        public async Task<IEnumerable<DoctorDTO>> GetAllAsync()
        {
            var result = await _doctorRepository.GetAllAsync();
            return result.Select(x => x.MapToDoctorDTO()).ToList();
        }

        public async Task<DoctorDTO> GetByIdAsync(Guid id)
        {
            var result = await _doctorRepository.GetByIdAsync(id);
            return result.MapToDoctorDTO();
        }
    }
}
