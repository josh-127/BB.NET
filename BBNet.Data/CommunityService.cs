using System.Collections.Generic;
using System.Linq;

namespace BBNet.Data
{
    public class CommunityService
    {
        private readonly BBNetDbContext context;

        public CommunityService(BBNetDbContext context)
            => this.context = context;

        public IEnumerable<Community> GetAllCommunities()
            => context.Communities;

        public Community GetCommunityById(int communityId)
            => context.Communities
                .Where(c => c.Id == communityId)
                .SingleOrDefault();

        public void AddCommunity(Community community)
        {
            context.Communities.Add(community);
            context.SaveChanges();
        }
    }
}