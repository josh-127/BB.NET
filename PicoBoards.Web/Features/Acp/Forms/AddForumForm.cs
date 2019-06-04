using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using PicoBoards.Forums.Models;

namespace PicoBoards.Web.Features.Acp.Forms
{
    public sealed class AddForumForm
    {
        public List<SelectListItem> Categories { get; }

        [Display(Name = "Parent Category")]
        [Required]
        public string Parent { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        public AddForumForm() { }

        public AddForumForm(CategoryRefCollection categories)
        {
            Categories = new List<SelectListItem>(
                from c in categories
                select new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                });
        }
    }
}