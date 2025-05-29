using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Validators.Customs
{
    public class ValidTimeRangeAttribute : ValidationAttribute
    {
        private readonly string _startTimeProperty;

        public ValidTimeRangeAttribute(string startTimeProperty)
        {
            _startTimeProperty = startTimeProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is TimeOnly endTime)
            {
                var startTimeProps = validationContext.ObjectType.GetProperty(_startTimeProperty);

                if(startTimeProps is null)
                {
                    return new ValidationResult("Not found Start Time attribute!");
                }   

                var startTimeValue = startTimeProps.GetValue(validationContext.ObjectInstance);

                if(startTimeValue is TimeOnly startTime)
                {
                    if(endTime <= startTime)
                    {
                        return new ValidationResult("End time to working must be greater start time!");
                    }
                }
            }

            return ValidationResult.Success;
        }

    }
}
