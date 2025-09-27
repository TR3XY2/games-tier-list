using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Domain.Interfaces
{
    public interface ITierListRepository
    {
        Task<TierList?> GetByIdAsync(int id);
        Task<TierList?> GetByUserIdAsync(int userId);
        Task AddAsync(TierList tierList);
        void Delete(TierList tierList);
    }
}
