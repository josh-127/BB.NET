using System.Collections.Generic;
using BBNet.Web.ViewModels.Shared;

namespace BBNet.Web.ViewModels.Forums
{
    public class ForumIndexViewModel
    {
        public ForumListingViewModel Forum { get; set; }

        public IEnumerable<TopicListingViewModel> Topics { get; set; }
    }
}