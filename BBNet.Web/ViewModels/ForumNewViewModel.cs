
using System.ComponentModel.DataAnnotations;

namespace BBNet.Web.ViewModels
{
    public class ForumNewViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}