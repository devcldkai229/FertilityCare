using FertilityCare.Domain.Interfaces.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.UnitOfWorks;

public interface ITreatmentUnitOfWork
{
    ITreatmentServiceRepository _treatmentServiceRepository { get; }

    ITreatmentCategoryRepository _treatmentCategoryRepository { get; }  

    ITreatmentStepRepository _treatmentStepRepository { get; }  
}
