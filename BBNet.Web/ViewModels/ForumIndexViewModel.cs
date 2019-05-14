using System.Collections.Generic;

namespace BBNet.Web.ViewModels
{
    public class ForumIndexViewModel
    {
        public ForumListingViewModel Forum { get; set; }

        public IEnumerable<TopicListingViewModel> Topics { get; set; }
    }
}