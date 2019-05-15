using System.Collections.Generic;
using BBNet.Web.ViewModels.Shared;

namespace BBNet.Web.ViewModels.Topics
{
    public class TopicIndexViewModel
    {
        public TopicListingViewModel Topic { get; set; }

        public IEnumerable<PostListingViewModel> Posts { get; set; }
    }
}