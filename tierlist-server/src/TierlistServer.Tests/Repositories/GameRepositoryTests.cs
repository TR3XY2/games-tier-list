using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TierlistServer.Domain.Entities;
using TierlistServer.Infrastructure.Data;
using TierlistServer.Infrastructure.Repositories;

namespace TierlistServer.Tests.Repositories
{
    public class GameRepositoryTests
    {
        private TierlistDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TierlistDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            return new TierlistDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddGame()
        {
            // Arrange
            var context = GetDbContext();
            var repo = new GameRepository(context);
            var game = new Game { Id = 1, Title = "Elden Ring", TierListId = 1 };

            // Act
            await repo.AddAsync(game);
            await context.SaveChangesAsync();

            // Assert
            var found = await repo.GetByIdAsync(1);
            Assert.NotNull(found);
            Assert.Equal("Elden Ring", found.Title);
        }

        [Fact]
        public async Task Delete_ShouldRemoveGame()
        {
            var context = GetDbContext();
            var repo = new GameRepository(context);

            var game = new Game { Id = 2, Title = "Dark Souls", TierListId = 1 };
            await repo.AddAsync(game);
            await context.SaveChangesAsync();

            // Act
            repo.Delete(game);
            await context.SaveChangesAsync();

            var found = await repo.GetByIdAsync(2);

            Assert.Null(found);
        }
    }
}
