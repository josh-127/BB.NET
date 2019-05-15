using System.Collections.Generic;
using BBNet.Web.ViewModels.Shared;

namespace BBNet.Web.ViewModels
{
    public class AdminForumsViewModel
    {
        public IEnumerable<ForumListingViewModel> Forums { get; set; }
    }
}