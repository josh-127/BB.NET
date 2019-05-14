using System.Collections.Generic;

namespace BBNet.Web.ViewModels
{
    public class AdminForumsViewModel
    {
        public IEnumerable<ForumListingViewModel> Forums { get; set; }
    }
}