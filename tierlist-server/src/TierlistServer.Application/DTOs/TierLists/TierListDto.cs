using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TierlistServer.Application.DTOs.TierLists
{
    public record TierListDto(
        int Id,
        int UserId
    );
}
