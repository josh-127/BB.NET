using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PicoBoards.Web
{
    public static class ModelStateDictionaryExtensions
    {
        public static void SetErrors(this ModelStateDictionary instance, ValidationResultCollection collection)
        {
            instance.Clear();
            foreach (var item in collection)
                instance.AddModelError("", item.ErrorMessage);
        }
    }
}