using System.Collections.Generic;

namespace BBNet.Data
{
    public interface IForumService
    {
        Forum GetForumById(int forumId);

        IEnumerable<Forum> GetAllForums();

        void AddForum(Forum forum);
    }
}