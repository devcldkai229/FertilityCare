using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Domain.Enums;
using FertilityCare.Infrastructure.Identity;
using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.DTOs.Appointments;
using FertilityCare.UseCase.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.UseCase.Mappers;

namespace FertilityCare.UseCase.Services
{
    public class AppointmentService : IAppointmentService
    {

        private readonly IAppointmentReminderRepository _reminderRepository;

        private readonly IAppointmentRepository _appointmentRepository;

        private readonly IPatientRepository _patientRepository;

        private readonly IPatientPartnerRepository _patientPartnerRepository;

        private readonly ILockableRepository<DoctorSchedule, long> _doctorScheduleRepository;

        private readonly IUserProfileRepository _userProfileRepository;

        public AppointmentService( 
                IAppointmentReminderRepository reminderRepository, 
                IAppointmentRepository appointmentRepository, 
                IPatientRepository patientRepository, 
                IPatientPartnerRepository patientPartnerRepository,
                ILockableRepository<DoctorSchedule, long> doctorScheduleRepository, 
                IUserProfileRepository userProfileRepository
            )
        {
            _reminderRepository = reminderRepository;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _patientPartnerRepository = patientPartnerRepository;
            _doctorScheduleRepository = doctorScheduleRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<AppointmentDTO> PlaceAppointmentLockAsync(PlaceAppointmentRequestDTO request)
        {
            using var transaction = await _appointmentRepository.BeginTransactionAsync();
            try
            {
                var schedule = await _doctorScheduleRepository.GetByIdWithLockAsync(request.DoctorScheduleId);
                if(schedule is null || !schedule.IsAvailable)
                {
                    await transaction.RollbackAsync();
                    throw new BookingAppointmentFailedExpception("Schedule to booking not exist or Unavailable!");
                }

                var currentAppointmentsCount = await _appointmentRepository.CountAsyncBySchedule(schedule.Id);

                if (currentAppointmentsCount > schedule.MaxAppointments)
                {
                    await transaction.RollbackAsync();
                    throw new AppointmentSlotLimitExceededException("Overslot of this schedule!");
                }

                var loadedUserProfile = await _userProfileRepository.FindByUserIdAsync(Guid.Parse(request.UserId));
                if (loadedUserProfile is null)
                {
                    await transaction.RollbackAsync();
                    throw new UserNotExistException($"User with id: {request.UserId} not exist!");
                }

                loadedUserProfile.FirstName = request.FirstName;
                loadedUserProfile.LastName = request.LastName;
                loadedUserProfile.MiddleName = request.MiddleName;
                loadedUserProfile.Address = request.Address;
                loadedUserProfile.DateOfBirth = request.DateOfBirth;

                await _userProfileRepository.UpdateInfoAsync(loadedUserProfile);

                var savedPatient = await _patientRepository.CreateAsync(new Patient
                {
                    UserProfile = loadedUserProfile,
                    UserProfileId = loadedUserProfile.Id,
                    MedicalHistory = "#NoData",
                    AllergiesNotes = "#NoData",
                    BloodType = "#NoData",
                    Height = null,
                    Weight = null,
                    MaritalStatus = request.PartnerFullName is not null ? "YES" : "NO"
                });

                var savedPartner = await _patientPartnerRepository.CreateAsync(new PatientPartner
                {
                    FullName = request.PartnerFullName,
                    ContactNumber = request.PartnerPhoneNumber,
                    Email = request.PartnerEmail,
                    DateOfBirth = request.DateOfBirth,
                    BloodType = "#NoData",
                    Gender = Gender.Male,
                    MedicalHistory = "#NoData",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                });

                var placedAppointment = await _appointmentRepository.CreateAsync(new Appointment
                {
                    Patient = savedPatient,
                    TreatmentServiceId = Guid.Parse(request.TreatmentServiceId),
                    DoctorId = Guid.Parse(request.DoctorId),
                    DoctorScheduleId = request.DoctorScheduleId,
                    PatientId = savedPatient.Id,
                    BookingEmail = request.BookingEmail,
                    BookingPhone = request.BookingPhone,
                    CreatedAt = DateTime.Now,
                    AppointmentDate = schedule.WorkDate.ToDateTime(schedule.StartTime),
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    Status = AppointmentStatus.Scheduled,
                    Purpose = "Consultant",
                    Note = request.Note,
                    CancellationReason = "#NoData",
                    UpdatedAt = null
                });

                var reminderBefore24h = await _reminderRepository.CreateAsync(new AppointmentReminder
                {
                    AppointmentId = placedAppointment.Id,
                    Appointment = placedAppointment,
                    Patient = savedPatient,
                    PatientId = savedPatient.Id,
                    ReminderDate = placedAppointment.AppointmentDate.AddDays(-1),
                    IsSent = false,
                    Note = "#NoData",
                    ReminderMethod = "Email",
                    Status = AppointmentReminderStatus.Pending,
                    CreatedAt = DateTime.Now,
                    SentAt = null,
                });

                await _appointmentRepository.CommitTransactionAsync();
                return placedAppointment.MapToAppointmentDTO();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new BookingAppointmentFailedExpception("Schedule to booking not exist or Unavailable!");
            }
        }

        public Task<IEnumerable<AppointmentDTO>> GetAppointmentByDoctorIdAsync(Guid doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppointmentDTO>> GetAppointmentByDoctorScheduleIdAsync(long scheduleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppointmentDTO>> GetAppointmentByPatientIdAsync(Guid patientId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppointmentDTO>> GetAppointmentByTreatmentServiceIdAsync(Guid treatmentServiceId)
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentDTO> UpdateAppointmentInfoAsync(UpdateAppointmentRequestDTO request)
        {
            throw new NotImplementedException();
        }

        }
 
}
