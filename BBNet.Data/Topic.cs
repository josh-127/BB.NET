using System;
using System.Collections.Generic;

namespace BBNet.Data
{
    public class Topic
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public virtual Forum Forum { get; set; }

        public virtual IList<Post> Posts { get; set; }
    }
}