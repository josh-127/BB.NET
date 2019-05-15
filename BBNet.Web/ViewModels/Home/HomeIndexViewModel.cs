using System.Collections.Generic;
using BBNet.Web.ViewModels.Shared;

namespace BBNet.Web.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<CommunityListingViewModel> Communities { get; set; }
    }
}