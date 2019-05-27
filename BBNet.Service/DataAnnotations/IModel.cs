using System.ComponentModel.DataAnnotations;

namespace BBNet.Service.DataAnnotations
{
    public interface IModel { }

    public static class IModelExtensions
    {
        public static ValidationResultCollection GetValidationResult(this IModel model)
        {
            var context = new ValidationContext(model);
            var results = new ValidationResultCollection();

            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        public static bool IsValid(this IModel model)
            => model.GetValidationResult().IsSuccessful;
    }
}