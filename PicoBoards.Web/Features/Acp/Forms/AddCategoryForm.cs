using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Web.Features.Acp.Forms
{
    public sealed class AddCategoryForm
    {
        [Required]
        public string Name { get; set; }
    }
}