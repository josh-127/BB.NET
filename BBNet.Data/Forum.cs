using System;
using System.Collections.Generic;

namespace BBNet.Data
{
    public class Forum
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime Created { get; set; }

        public virtual IList<Topic> Topics { get; set; }
    }
}