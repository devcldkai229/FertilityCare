using FertilityCare.UseCase.DTOs.TreatmentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Interfaces
{
    public interface ITreatmentService
    {
        Task<TreatmentServiceDTO> GetById(Guid Id);

        Task<IEnumerable<TreatmentServiceDTO>> GetAll();

    }
}
