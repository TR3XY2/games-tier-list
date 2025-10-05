using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TierlistServer.Domain.Entities;
using TierlistServer.Domain.Interfaces;
using TierlistServer.Infrastructure;
using TierlistServer.Infrastructure.Data;
using TierlistServer.Infrastructure.Repositories;
using Xunit;

namespace TierlistServer.Tests.Infrastructure
{
    public class UnitOfWorkTests
    {
        private static UnitOfWork GetUnitOfWork()
        {
            var options = new DbContextOptionsBuilder<TierlistDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            var context = new TierlistDbContext(options);

            var gameRepo = new GameRepository(context);
            var tierRepo = new TierListRepository(context);
            var userRepo = new UserRepository(context);

            return new UnitOfWork(context, gameRepo, tierRepo, userRepo);
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldCommitAcrossRepositories()
        {
            var uow = GetUnitOfWork();

            var user = new User { Id = 1, Email = "player@example.com", Username = "Player1" };
            await uow.Users.AddAsync(user);

            var tierList = new TierList { Id = 1, UserId = 1 };
            await uow.TierLists.AddAsync(tierList);

            var game = new Game { Id = 1, Title = "Elden Ring", TierListId = 1 };
            await uow.Games.AddAsync(game);

            await uow.SaveChangesAsync();

            var foundUser = await uow.Users.GetByIdAsync(1);
            var foundTierList = await uow.TierLists.GetByIdAsync(1);
            var foundGame = await uow.Games.GetByIdAsync(1);

            Assert.NotNull(foundUser);
            Assert.NotNull(foundTierList);
            Assert.NotNull(foundGame);

            Assert.Equal("player@example.com", foundUser.Email);
            Assert.Equal(1, foundTierList.UserId);
            Assert.Equal("Elden Ring", foundGame.Title);
        }
    }
}
