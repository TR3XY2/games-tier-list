using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TierlistServer.Application.Interfaces;
using TierlistServer.Domain.Entities;
using TierlistServer.Domain.Interfaces;

namespace TierlistServer.Application.Services;

public class GameService : IGameService
{
    private readonly IUnitOfWork unitOfWork;

    public GameService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Game> AddAsync(Game game)
    {
        await this.unitOfWork.Games.AddAsync(game);
        await this.unitOfWork.SaveChangesAsync();

        return game;
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        return await this.unitOfWork.Games.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Game>> GetByTierListIdAsync(int tierListId)
    {
        return await this.unitOfWork.Games.GetByTierListIdAsync(tierListId);
    }

    public async Task<bool> UpdateTierAsync(int gameId, string? tier)
    {
        var game = await unitOfWork.Games.GetByIdAsync(gameId);

        if (game == null)
        {
            return false;
        }

        game.Tier = tier;

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int gameId)
    {
        var game = await unitOfWork.Games.GetByIdAsync(gameId);

        if (game == null)
        {
            return false;
        }

        unitOfWork.Games.Delete(game);

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
