using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.UseCase.DTOs.TreatmentProgressReport;

namespace FertilityCare.UseCase.Interfaces
{
    public interface ITreatmentProgressReportService
    {
        Task<TreatmentProgressReportDTO> GetAllAsync();
        Task<TreatmentProgressReportDTO> GetIdAsync();
        Task<TreatmentProgressReportDTO> Create
    }
}
