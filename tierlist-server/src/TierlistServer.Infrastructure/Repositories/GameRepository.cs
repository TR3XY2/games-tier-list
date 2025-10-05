using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TierlistServer.Domain.Entities;
using TierlistServer.Domain.Interfaces;
using TierlistServer.Infrastructure.Data;

namespace TierlistServer.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly TierlistDbContext _context;

        public GameRepository(TierlistDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
        }

        public void Delete(Game game)
        {
            _context.Games.Remove(game);
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public async Task<IEnumerable<Game>> GetByTierListIdAsync(int tierListId)
        {
            return await _context.Games.Where(g => g.TierListId == tierListId).ToListAsync();
        }
    }
}
