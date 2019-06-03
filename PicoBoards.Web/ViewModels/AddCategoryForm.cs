using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Web.ViewModels
{
    public sealed class AddCategoryForm
    {
        [Required]
        public string Name { get; set; }
    }
}