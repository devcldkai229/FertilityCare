using FertilityCare.Domain.Entities;
using FertilityCare.UseCase.DTOs.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Mappers
{
    public static class PatientMapper
    {

        public static PatientDTO MapToPatientDTO(this Patient model)
        {
            return new PatientDTO
            {
                FirstName = model.UserProfile.FirstName,
                MiddleName = model.UserProfile.MiddleName,
                LastName = model.UserProfile.LastName,
                Gender = model.UserProfile.Gender.ToString(),
                DateOfBirth = model.UserProfile.DateOfBirth?.ToString("dd/MM/yyyy"),
                Address = model.UserProfile.Address,
                City = model.UserProfile.City,
                Province = model.UserProfile.Province,
                Country = model.UserProfile.Country,
                AvatarUrl = model.UserProfile.AvatarUrl,
                MedicalHistory = model.MedicalHistory,
                FertilityDiagnosis = model.FertilityDiagnosis,
                AllergiesNotes = model.AllergiesNotes,
                BloodType = model.BloodType,
                Height = model.Height,
                Weight = model.Weight,
                MaritalStatus = model.MaritalStatus,
                PartnerFullName = model.PatientPartner.FullName,
                CreatedAt = model.UserProfile?.CreatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),
            };
        }

    }
}
