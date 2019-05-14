using System.Collections.Generic;

namespace BBNet.Data
{
    public interface IPostService
    {
        Post GetPostById(int postId);

        IEnumerable<Post> GetAllPosts();

        IEnumerable<Post> GetPostsByTopicId(int topicId);

        void AddPost(Post post, Topic topic);
    }
}