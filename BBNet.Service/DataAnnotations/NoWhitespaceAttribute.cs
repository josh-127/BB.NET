using System;
using System.ComponentModel.DataAnnotations;

namespace BBNet.Service.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NoWhitespaceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string s)
            {
                if (s.Contains(" ") || s.Contains("\t") || s.Contains("\r") || s.Contains("\n"))
                    return new ValidationResult("Property contains whitespace.");

                return null;
            }

            return new ValidationResult("Property must be a string.");
        }
    }
}