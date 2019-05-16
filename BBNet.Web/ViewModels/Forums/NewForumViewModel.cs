using System.ComponentModel.DataAnnotations;

namespace BBNet.Web.ViewModels.Forums
{
    public class NewForumViewModel
    {
        public int CommunityId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}