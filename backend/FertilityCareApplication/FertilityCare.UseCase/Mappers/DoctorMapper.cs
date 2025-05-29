using FertilityCare.Domain.Entities;
using FertilityCare.UseCase.DTOs.Doctors;
using FertilityCare.UseCase.DTOs.DoctorSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Mappers
{
    public static class DoctorMapper
    {

        public static DoctorDTO MapToDoctorDTO(this Doctor model)
        {
            return new DoctorDTO
            {
                Id = model.Id.ToString(),
                FirstName = model.UserProfile.FirstName,
                LastName = model.UserProfile.LastName,
                MiddleName = model.UserProfile.MiddleName,
                Gender = model.UserProfile.Gender.ToString(),
                DateOfBirth = model.UserProfile.DateOfBirth?.ToString("dd/MM/yyyy"),
                Degree = model.Degree,
                Specialization = model.Specialization,
                YearsOfExperience = model.YearsOfExperience,
                Biography = model.Biography,
                Education = model.Education,
                Rating = model.Rating,
                PatientsServed = model.PatientsServed,
                IsAcceptingPatients = model.IsAcceptingPatients,
                Address = model.UserProfile?.Address,
                AvatarUrl = model.UserProfile?.AvatarUrl,
                CreatedAt = model.UserProfile?.CreatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),
                DoctorSchedules = model.DoctorSchedules?.Select(x => x.MapToDoctorScheduleDTO()).ToList()
            };
        }

    }
}
