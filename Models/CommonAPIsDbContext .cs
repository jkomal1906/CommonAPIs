using Microsoft.EntityFrameworkCore;

namespace CommonAPIs.Models
{
    public class CommonAPIsDbContext : DbContext
    {
        public CommonAPIsDbContext(DbContextOptions<CommonAPIsDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
