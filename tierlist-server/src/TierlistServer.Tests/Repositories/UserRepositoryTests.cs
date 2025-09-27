using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TierlistServer.Domain.Entities;
using TierlistServer.Infrastructure.Data;
using TierlistServer.Infrastructure.Repositories;
using Xunit;

namespace TierlistServer.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private TierlistDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TierlistDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            return new TierlistDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser()
        {
            var context = GetDbContext();
            var repo = new UserRepository(context);
            var user = new User { Id = 1, Email = "test@example.com", Username = "John228" };

            await repo.AddAsync(user);
            await context.SaveChangesAsync();

            var found = await repo.GetByIdAsync(1);

            Assert.NotNull(found);
            Assert.Equal("test@example.com", found.Email);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnUser()
        {
            var context = GetDbContext();
            var repo = new UserRepository(context);
            var user = new User { Id = 2, Email = "jane@example.com", Username = "Jane123" };

            await repo.AddAsync(user);
            await context.SaveChangesAsync();

            var found = await repo.GetByEmailAsync("jane@example.com");

            Assert.NotNull(found);
            Assert.Equal("Jane123", found.Username);
        }
    }
}
