using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PicoBoards.Web.Features.Acp.Forms
{
    public sealed class EditCategoryForm
    {
        [HiddenInput]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public EditCategoryForm() { }

        public EditCategoryForm(int categoryId)
            => CategoryId = categoryId;
    }
}