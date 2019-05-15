using System.Collections.Generic;

namespace BBNet.Web.ViewModels
{
    public class CommunityIndexViewModel
    {
        public IEnumerable<ForumListingViewModel> Forums { get; set; }
    }
}