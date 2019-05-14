using System.Collections.Generic;
using System.Linq;

namespace BBNet.Data
{
    public class PostService
    {
        private readonly BBNetDbContext context;

        public PostService(BBNetDbContext context)
            => this.context = context;

        public Post GetPostById(int postId)
            => context.Posts
                .Where(p => p.Id == postId)
                .SingleOrDefault();

        public IEnumerable<Post> GetAllPosts()
            => context.Posts;

        public IEnumerable<Post> GetPostsByTopicId(int topicId)
            => context.Posts
                .Where(p => p.Topic.Id == topicId);

        public void AddPost(Post post, Topic topic)
        {
            post.Topic = topic;
            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}