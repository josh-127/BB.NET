using System;

namespace BBNet.Data
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }

        public virtual Topic Topic { get; set; }

        public virtual BBNetUser Author { get; set; }
    }
}