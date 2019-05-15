using System.ComponentModel.DataAnnotations;

namespace BBNet.Web.ViewModels.Communities
{
    public class CommunityNewViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}