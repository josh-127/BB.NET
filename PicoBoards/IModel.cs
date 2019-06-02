using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PicoBoards
{
    public interface IModel { }

    public static class IModelExtensions
    {
        public static string GetDisplayName(this IModel model, string propertyName)
            => (model
                .GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .First() as DisplayAttribute)
                .Name;
    }
}