using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Validators.Customs
{
    public class ValidAgeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateOnly dateOfBirth)
            {
                int age = DateTime.Today.Year - dateOfBirth.Year;
                if(age < 18)
                {
                    return new ValidationResult("Age must be greater 18 year old to perform this action!");
                }
            }

            return ValidationResult.Success;
        }

    }
}
    