using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TierlistServer.Domain.Entities;
using TierlistServer.Infrastructure.Data;
using TierlistServer.Infrastructure.Repositories;
using Xunit;

namespace TierlistServer.Tests.Repositories
{
    public class TierListRepositoryTests
    {
        private static TierlistDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TierlistDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            return new TierlistDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddTierList()
        {
            var context = GetDbContext();
            var repo = new TierListRepository(context);
            var tierList = new TierList { Id = 1, UserId = 10 };

            await repo.AddAsync(tierList);
            await context.SaveChangesAsync();

            var found = await repo.GetByIdAsync(1);

            Assert.NotNull(found);
            Assert.Equal(10, found.UserId);
        }
    }
}
