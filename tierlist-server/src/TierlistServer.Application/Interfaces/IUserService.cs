using TierlistServer.Domain.Entities;

namespace TierlistServer.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> LoginAsync(string email, string password);
        Task<User> RegisterAsync(string email, string username, string password);
    }
}