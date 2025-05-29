using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Identity;
using FertilityCare.Infrastructure.Repositories;
using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.DTOs.Appointments;
using FertilityCare.UseCase.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IApplicationUserRepository<ApplicationUser> _userManager;

        private readonly IAppointmentReminderRepository _reminderRepository;

        private readonly IAppointmentRepository _appointmentRepository;

        private readonly IPatientRepository _patientRepository;

        private readonly IPatientPartnerRepository _patientPartnerRepository;

        private readonly ILockableRepository<DoctorSchedule, long> _doctorScheduleRepository;

        private readonly IDoctorRepository _doctorRepository;

        private readonly IUserProfileRepository _userProfileRepository;

        public AppointmentService(IApplicationUserRepository<ApplicationUser> userManager, 
            IAppointmentReminderRepository reminderRepository, 
            IAppointmentRepository appointmentRepository, 
            IPatientRepository patientRepository, 
            IPatientPartnerRepository patientPartnerRepository,
            ILockableRepository<DoctorSchedule, long> doctorScheduleRepository, 
            IDoctorRepository doctorRepository, 
            IUserProfileRepository userProfileRepository)
        {
            _userManager = userManager;
            _reminderRepository = reminderRepository;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _patientPartnerRepository = patientPartnerRepository;
            _doctorScheduleRepository = doctorScheduleRepository;
            _doctorRepository = doctorRepository;
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

                var loadedAppointments = await _appointmentRepository.GetAllAsync();
                int bookingCount = loadedAppointments.Count();


            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();
                throw new BookingAppointmentFailedExpception("Schedule to booking not exist or Unavailable!");
            }

            return null;
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
