using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TierlistServer.Application.DTOs.Games;
using TierlistServer.Application.DTOs.TierLists;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Game, AddGameDto>().ReverseMap();
            this.CreateMap<TierList, TierListDto>().ReverseMap();
        }
    }
}
