using System;
using BBNet.Data;

namespace BBNet.Web.ViewModels
{
    public class PostListingViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }
    }

    public static class PostExtensions
    {
        public static PostListingViewModel ToPostListing(this Post post)
            => new PostListingViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Created = post.Created
            };
    }
}