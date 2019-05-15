using BBNet.Data;

namespace BBNet.Web.ViewModels
{
    public class CommunityListingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public static class CommunityExtensions
    {
        public static CommunityListingViewModel ToCommunityListing(this Community community)
            => new CommunityListingViewModel
            {
                Id = community.Id,
                Name = community.Name
            };
    }
}