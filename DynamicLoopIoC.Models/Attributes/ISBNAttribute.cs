using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DynamicLoopIoC.Common.Helpers;

namespace DynamicLoopIoC.Models.Attributes
{
    public class ISBNAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string isbn = ISBNFilter.Filter((string)value);
            if (isbn.Length != 13)
                return new ValidationResult("ISBN has incorrect length (should be 13 digits)", new List<string> { validationContext.MemberName });
            return ValidationResult.Success;
        }
    }
}