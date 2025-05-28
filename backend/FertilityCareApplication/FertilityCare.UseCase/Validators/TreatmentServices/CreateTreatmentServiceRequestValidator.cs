using FertilityCare.UseCase.DTOs.TreatmentServices;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Validators.TreatmentServices
{
    public class CreateTreatmentServiceRequestValidator : AbstractValidator<CreateTreatmentServiceRequestDTO>
    {

        public CreateTreatmentServiceRequestValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required");

            RuleFor(x => x.ServiceName).NotEmpty().WithMessage("Service's name is required").MaximumLength(200);

            RuleFor(x => x.BasicPrice).GreaterThan(0).WithMessage("Treatment service's basic price is required");
        }

    }
}