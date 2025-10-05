using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TierlistServer.Domain.Interfaces;
using TierlistServer.Infrastructure.Data;

namespace TierlistServer.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TierlistDbContext _context;

        public IGameRepository Games { get; }
        public ITierListRepository TierLists { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(
            TierlistDbContext context,
            IGameRepository gameRepository,
            ITierListRepository tierListRepository,
            IUserRepository userRepository)
        {
            _context = context;
            Games = gameRepository;
            TierLists = tierListRepository;
            Users = userRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
