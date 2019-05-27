using System.ComponentModel.DataAnnotations;

namespace BBNet.Service.Models
{
    public static class ObjectExtensions
    {
        public static ValidationResultCollection GetValidationResult(this object instance)
        {
            var context = new ValidationContext(instance);
            var results = new ValidationResultCollection();

            Validator.TryValidateObject(instance, context, results);
            return results;
        }

        public static bool IsValid(this object instance)
            => instance.GetValidationResult().IsSuccessful;
    }
}