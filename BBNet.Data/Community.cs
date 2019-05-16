using System.Collections.Generic;

namespace BBNet.Data
{
    public class Community
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IList<Forum> Forums { get; set; }

        public virtual IList<Member> Members { get; set; }
    }
}