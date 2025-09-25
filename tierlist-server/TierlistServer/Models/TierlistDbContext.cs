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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
               .HasOne(u => u.TierList)
               .WithOne(t => t.User)
               .HasForeignKey<TierList>(t => t.UserId);

            modelBuilder.Entity<TierList>()
                .HasMany(t => t.Games)
                .WithOne(g => g.TierList)
                .HasForeignKey(g => g.TierListId);
        }
    }
}
