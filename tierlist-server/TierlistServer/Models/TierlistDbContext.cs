using Microsoft.EntityFrameworkCore;

namespace TierlistServer.Models
{
    public class TierlistDbContext : DbContext
    {
        public TierlistDbContext(DbContextOptions<TierlistDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TierList> TierLists { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
