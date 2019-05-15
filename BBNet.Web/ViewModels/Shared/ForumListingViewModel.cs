using BBNet.Data;

namespace BBNet.Web.ViewModels.Shared
{
    public class ForumListingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }

    public static class ForumExtensions
    {
        public static ForumListingViewModel ToForumListing(this Forum forum)
            => new ForumListingViewModel
            {
                Id = forum.Id,
                Name = forum.Name,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
    }
}