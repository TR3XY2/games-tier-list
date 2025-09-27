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
    public class UserRepository : IUserRepository
    {
        private readonly TierlistDbContext _context;

        public UserRepository(TierlistDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }
    }
}
