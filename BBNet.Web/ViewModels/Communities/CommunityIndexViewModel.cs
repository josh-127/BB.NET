using System.Collections.Generic;
using BBNet.Web.ViewModels.Shared;

namespace BBNet.Web.ViewModels.Communities
{
    public class CommunityIndexViewModel
    {
        public IEnumerable<ForumListingViewModel> Forums { get; set; }
    }
}