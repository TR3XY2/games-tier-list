using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TierlistServer.Application.DTOs.Games;
using TierlistServer.Application.Interfaces;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameService gamesService;
    private readonly ITierListService tierListService;
    private readonly IMapper mapper;

    public GamesController(IGameService gamesService, IMapper mapper, ITierListService tierListService)
    {
        this.gamesService = gamesService;
        this.mapper = mapper;
        this.tierListService = tierListService;
    }


    [HttpGet]
    public async Task<IActionResult> GetMyGames()
    {
        var userId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var tierlist = await this.tierListService.GetByUserIdAsync(userId);

        if (tierlist == null)
        {
            return NotFound();
        }

        var games = await this.gamesService.GetByTierListIdAsync(tierlist!.Id);

        var dtos = this.mapper.Map<IEnumerable<GameDto>>(games);

        return this.Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var game = await this.gamesService.GetByIdAsync(id);

        if (game == null)
        {
            return this.NotFound();
        }

        var dto = this.mapper.Map<GameDto>(game);

        return this.Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> AddGame([FromBody] AddGameDto addGameDto)
    {
        var userId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var tierlist = await this.tierListService.GetByUserIdAsync(userId);

        if (tierlist == null)
        {
            return this.NotFound();
        }

        var game = mapper.Map<AddGameDto, Game>(addGameDto);
        game.TierListId = tierlist.Id;

        var result = await this.gamesService.AddAsync(game);

        var gameDto = mapper.Map<Game, GameDto>(result);

        return this.CreatedAtAction(nameof(GetById), new { id = result.Id }, gameDto);
    }

    [HttpPut("{id}/tier")]
    public async Task<IActionResult> UpdateTier(int id, [FromBody] UpdateGameTierDto updateGameTierDto)
    {
        var IsSuccessfull = await gamesService.UpdateTierAsync(id, updateGameTierDto.Tier);

        if (!IsSuccessfull)
        {
            return this.NotFound();
        }

        return this.NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var success = await gamesService.DeleteAsync(id);

        if (!success)
        {
            return this.NotFound();
        }

        return this.NoContent();
    }
}
