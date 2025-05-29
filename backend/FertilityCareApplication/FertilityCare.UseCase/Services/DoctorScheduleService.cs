using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.UseCase.DTOs.DoctorSchedules;
using FertilityCare.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.UseCase.Mappers;

namespace FertilityCare.UseCase.Services
{
    public class DoctorScheduleService : IDoctorScheduleService
    {

        private readonly IDoctorScheduleRepository _doctorScheduleRepository;

        public DoctorScheduleService(IDoctorScheduleRepository doctorScheduleRepository)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
        }

        public async Task<DoctorScheduleDTO> AddWorkScheduleAsync(CreateDoctorScheduleRequestDTO request)
        {
            var result = await _doctorScheduleRepository.CreateAsync(new DoctorSchedule
                        {
                            DoctorId = Guid.Parse(request.DoctorId),
                            WorkDate = request.WorkDate,
                            StartTime = request.StartTime,
                            EndTime = request.EndTime,
                            Note = request.Note,
                            MaxAppointments = 10,
                            IsAvailable = true
                        });

            return result.MapToDoctorScheduleDTO();
        }

        public async Task<DoctorScheduleDTO> GetByIdAsync(long id)
        {
            var result = await _doctorScheduleRepository.GetByIdAsync(id);
            return result.MapToDoctorScheduleDTO();
        }

        public async Task<IEnumerable<DoctorScheduleDTO>> GetWorkScheduleByDoctorIdAsync(Guid doctorId)
        {
            var result = await _doctorScheduleRepository.GetByDoctorIdAsync(doctorId);
            return result.Select(x => x.MapToDoctorScheduleDTO()).ToList();
        }
    }
}
