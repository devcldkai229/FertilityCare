using FertilityCare.Domain.Entities;
using FertilityCare.UseCase.DTOs.DoctorSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Mappers
{
    public static class DoctorScheduleMapper
    {

        public static DoctorScheduleDTO MapToDoctorScheduleDTO(this DoctorSchedule model)
        {
            return new DoctorScheduleDTO
            {
                Id = model.Id,
                WorkDate = model.WorkDate.ToString("dd/MM/yyyy"),
                StartTime = model.StartTime.ToString("dd/MM/yyyy"),
                EndTime = model.EndTime.ToString("dd/MM/yyyy"),
                IsAvailable = model.IsAvailable,
                MaxAppointments = model.MaxAppointments,
                Note = model.Note,
            };
        }

    }
}
