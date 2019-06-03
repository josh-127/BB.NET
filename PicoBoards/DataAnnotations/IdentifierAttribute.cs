using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PicoBoards.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class IdentifierAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
            => IsValid(value)
                ? null
                : new ValidationResult(
                    $"{context.DisplayName} can only contain alphanumeric characters and '_'.");

        public bool IsValid(object value)
            => value is string str && Regex.IsMatch(str, "^[A-Za-z0-9_]+$");
    }
}