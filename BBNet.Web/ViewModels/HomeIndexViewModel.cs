using System.Collections.Generic;

namespace BBNet.Web.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<CommunityListingViewModel> Communities { get; set; }
    }
}