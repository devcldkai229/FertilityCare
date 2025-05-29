using FertilityCare.UseCase.DTOs.Patient;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Validators.TreatmentServices
{
    public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequestDTO>
    {

        public CreatePatientRequestValidator() 
        { 
            
        
        }

    }
}
