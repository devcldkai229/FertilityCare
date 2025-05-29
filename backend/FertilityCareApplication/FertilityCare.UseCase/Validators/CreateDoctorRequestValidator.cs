using FertilityCare.UseCase.DTOs.Doctors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Validators
{
    public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequestDTO>
    {
        public CreateDoctorRequestValidator()
        { 
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required!");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password is greater 6 characters!");

            RuleFor(x => x.PhoneNumber).MaximumLength(12).WithMessage("Phone is less than 12 numbers!");

            RuleFor(x => x.FirstName).MaximumLength(50);

            RuleFor(x => x.MiddleName).MaximumLength(50);
            
            RuleFor(x => x.LastName).MaximumLength(50);
        }

    }
}
