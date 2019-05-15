﻿using System.Collections.Generic;

namespace BBNet.Data
{
    public class Community
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IList<Forum> Forums { get; set; }

        public virtual IList<BBNetUser> Users { get; set; }
    }
}