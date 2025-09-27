using Microsoft.AspNetCore.Identity;
using TierlistServer.Domain.Entities;
using TierlistServer.Domain.Interfaces;

namespace TierlistServer.Application.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork uow, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = uow;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> RegisterAsync(string email, string username, string password)
        {
            var existing = await _unitOfWork.Users.GetByEmailAsync(email);
            if (existing != null)
            {
                throw new InvalidOperationException("Email already registered.");
            }

            var user = new User
            {
                Email = email,
                Username = username,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            await _unitOfWork.Users.AddAsync(user);

            var tierList = new TierList
            {
                User = user,
            };

            await _unitOfWork.TierLists.AddAsync(tierList);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(email);
            if (user == null || !VerifyPassword(user, password))
            {
                return null;
            }
            return user;
        }

        public bool VerifyPassword(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
