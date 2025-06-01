using Microsoft.EntityFrameworkCore;

namespace CommonAPIs.Models
{
    public class CommomAPIsDbContext : DbContext
    {
        public CommomAPIsDbContext(DbContextOptions<CommomAPIsDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
