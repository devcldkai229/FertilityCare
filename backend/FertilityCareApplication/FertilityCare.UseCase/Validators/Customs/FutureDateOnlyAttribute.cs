using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Validators.Customs
{
    public class FutureDateOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateOnly date)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                if(date < today)
                {
                    return new ValidationResult("Working date must be after or equal today!");
                }
            }

            return ValidationResult.Success;
        }

    }
}
