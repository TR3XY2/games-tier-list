using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task<Game?> GetByIdAsync(int id);
        Task<IEnumerable<Game>> GetByTierListIdAsync(int tierListId);
        Task AddAsync(Game game);
        void Delete(Game game);
    }   
}
