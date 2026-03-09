using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TierlistServer.Application.Interfaces;
using TierlistServer.Domain.Entities;
using TierlistServer.Domain.Interfaces;

namespace TierlistServer.Application.Services;

public class TierListService : ITierListService
{
    private readonly IUnitOfWork unitOfWork;

    public TierListService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<TierList?> GetByUserIdAsync(int userId)
    {
        return await this.unitOfWork.TierLists.GetByUserIdAsync(userId);
    }

    public async Task<TierList> CreateForUserAsync(int userId)
    {
        var existing = await unitOfWork.TierLists.GetByUserIdAsync(userId);

        if (existing != null)
        {
            return existing;
        }

        var tierList = new TierList
        {
            UserId = userId
        };

        await unitOfWork.TierLists.AddAsync(tierList);
        await unitOfWork.SaveChangesAsync();

        return tierList;
    }
}
