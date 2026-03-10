using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TierlistServer.Application.DTOs.TierLists;
using TierlistServer.Application.Interfaces;
using TierlistServer.Domain.Entities;
using TierlistServer.Infrastructure;

namespace TierlistServer.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
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

    [HttpGet]
    public async Task<IActionResult> GetMyTierList()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var tierlist = await this.tierListService.GetByUserIdAsync(userId);

        if (tierlist == null)
        {
            return this.NotFound();
        }

        var dto = this.mapper.Map<TierList, TierListDto>(tierlist);

        return this.Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateForCurrentUser()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var tierlist = await this.tierListService.CreateForUserAsync(userId);

        var dto = this.mapper.Map<TierList, TierListDto>(tierlist);

        return this.Ok(dto);
    }
}
