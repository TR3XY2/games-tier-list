using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TierlistServer.Application.DTOs.TierLists;
using TierlistServer.Application.Interfaces;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TierListController : ControllerBase
    {
        private readonly ITierListService tierListService;
        private readonly IMapper mapper;

        public TierListController(ITierListService tierListService, IMapper mapper)
        {
            this.tierListService = tierListService;
            this.mapper = mapper;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        { 
            var tierlist = await this.tierListService.GetByUserIdAsync(userId);

            if (tierlist == null)
            {
                return this.NotFound();
            }

            var dto = this.mapper.Map<TierList, TierListDto>(tierlist);

            return this.Ok(dto);
        }

        [HttpPost("user/{userId}")]
        public async Task<IActionResult> CreateForUser(int userId)
        {
            var tierlist = await this.tierListService.CreateForUserAsync(userId);

            var dto = this.mapper.Map<TierList, TierListDto>(tierlist);

            return this.Ok(dto);
        }
    }
}
