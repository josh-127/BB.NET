using System.Collections.Generic;

namespace BBNet.Web.ViewModels
{
    public class TopicIndexViewModel
    {
        public TopicListingViewModel Topic { get; set; }

        public IEnumerable<PostListingViewModel> Posts { get; set; }
    }
}