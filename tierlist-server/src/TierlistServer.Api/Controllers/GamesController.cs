using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TierlistServer.Application.DTOs.Games;
using TierlistServer.Application.Interfaces;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService gamesService;
        private readonly IMapper mapper;

        public GamesController(IGameService gamesService, IMapper mapper)
        {
            this.gamesService = gamesService;
            this.mapper = mapper;
        }


        [HttpGet("tierlist/{tierListId}")]
        public async Task<IActionResult> GetGamesByTierListId([FromRoute] int tierListId)
        {
            var games = await this.gamesService.GetByTierListIdAsync(tierListId);

            var dtos = this.mapper.Map<IEnumerable<GameDto>>(games);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] AddGameDto addGameDto)
        {
            var game = mapper.Map<AddGameDto, Game>(addGameDto);
            var result = await this.gamesService.AddAsync(game);

            var gameDto = mapper.Map<Game, GameDto>(result);

            return Ok(gameDto);
        }

        [HttpPut("{id}/tier")]
        public async Task<IActionResult> UpdateTier(int id, [FromBody] UpdateGameTierDto updateGameTierDto)
        {
            var IsSuccessfull = await gamesService.UpdateTierAsync(id, updateGameTierDto.Tier);

            if (!IsSuccessfull)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var success = await gamesService.DeleteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
