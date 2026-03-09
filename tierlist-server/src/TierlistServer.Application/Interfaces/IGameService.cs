using TierlistServer.Domain.Entities;

namespace TierlistServer.Application.Interfaces;

public interface IGameService
{
    Task<Game> AddAsync(Game game);
    Task<bool> DeleteAsync(int gameId);
    Task<Game?> GetByIdAsync(int id);
    Task<IEnumerable<Game>> GetByTierListIdAsync(int tierListId);
    Task<bool> UpdateTierAsync(int gameId, string? tier);
}