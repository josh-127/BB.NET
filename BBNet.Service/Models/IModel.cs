using System.ComponentModel.DataAnnotations;

namespace BBNet.Service.Models
{
    public interface IModel { }

    public static class IModelExtensions
    {
        public static ValidationResultCollection GetValidationResult(this IModel model)
        {
            var context = new ValidationContext(model);
            var results = new ValidationResultCollection();

            Validator.TryValidateObject(model, context, results);
            return results;
        }

        public static bool IsValid(this IModel model)
            => model.GetValidationResult().IsSuccessful;
    }
}