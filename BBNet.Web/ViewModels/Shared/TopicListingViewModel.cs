using System;
using BBNet.Data;

namespace BBNet.Web.ViewModels.Shared
{
    public class TopicListingViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }
    }

    public static class TopicExtensions
    {
        public static TopicListingViewModel ToTopicListing(this Topic topic)
            => new TopicListingViewModel
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                Created = topic.Created
            };
    }
}