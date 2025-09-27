using Microsoft.EntityFrameworkCore;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Infrastructure.Data
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
                .HasIndex(u => u.Email)
                .IsUnique();

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
