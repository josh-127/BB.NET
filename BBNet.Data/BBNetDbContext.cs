using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BBNet.Data
{
    public class BBNetDbContext : IdentityDbContext<User>
    {
        public BBNetDbContext(DbContextOptions<BBNetDbContext> options) :
            base(options)
        { }

        public DbSet<Forum> Forums { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}