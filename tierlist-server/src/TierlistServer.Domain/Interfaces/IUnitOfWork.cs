using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TierlistServer.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGameRepository Games { get; }
        ITierListRepository TierLists { get; }
        IUserRepository Users { get; }

        Task<int> SaveChangesAsync();
    }
}
