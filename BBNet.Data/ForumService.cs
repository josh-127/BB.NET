using System.Collections.Generic;
using System.Linq;

namespace BBNet.Data
{
    public class ForumService : IForumService
    {
        private readonly BBNetDbContext context;

        public ForumService(BBNetDbContext context)
            => this.context = context;

        public IEnumerable<Forum> GetAllForums()
            => context.Forums;

        public Forum GetForumById(int forumId)
            => context.Forums
                .Where(f => f.Id == forumId)
                .SingleOrDefault();

        public void AddForum(Forum forum)
        {
            context.Forums.Add(forum);
            context.SaveChanges();
        }
    }
}