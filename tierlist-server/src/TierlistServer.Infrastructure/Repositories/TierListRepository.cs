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
    public class TierListRepository : ITierListRepository
    {
        private readonly TierlistDbContext _context;

        public TierListRepository(TierlistDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TierList tierList)
        {
            await _context.TierLists.AddAsync(tierList);
        }

        public async Task<TierList?> GetByIdAsync(int id)
        {
            return await _context.TierLists.FindAsync(id);
        }

        public async Task<TierList?> GetByUserIdAsync(int userId)
        {
            return await _context.TierLists.FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public void Delete(TierList tierList)
        {
            _context.TierLists.Remove(tierList);
        }
    }
}
