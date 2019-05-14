using System;

namespace BBNet.Web.ViewModels
{
    public class TopicListingViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }
    }
}