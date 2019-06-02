using System.ComponentModel.DataAnnotations;

namespace PicoBoards
{
    public interface IValidatable { }

    public static class IValidatableExtensions
    {
        public static ValidationResultCollection GetValidationResult(this IValidatable validatable)
        {
            var context = new ValidationContext(validatable);
            var results = new ValidationResultCollection();

            Validator.TryValidateObject(validatable, context, results, true);
            return results;
        }

        public static bool IsValid(this IValidatable validatable)
            => validatable.GetValidationResult().IsValid;
    }
}