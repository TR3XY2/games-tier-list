using TierlistServer.Domain.Entities;

namespace TierlistServer.Application.Interfaces
{
    public interface ITierListService
    {
        Task<TierList> CreateForUserAsync(int userId);
        Task<TierList?> GetByUserIdAsync(int userId);
    }
}