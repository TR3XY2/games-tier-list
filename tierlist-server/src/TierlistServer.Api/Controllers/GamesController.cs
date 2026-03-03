using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TierlistServer.Application.Services;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GamesService gamesService;

        public GamesController(GamesService gamesService)
        {
            this.gamesService = gamesService;
        }


        [HttpGet("tierlist/{tierListId}")]
        public async Task<IActionResult> GetGamesByTierListId([FromRoute] int tierListId)
        {
            var games = await this.gamesService.GetByTierListIdAsync(tierListId);

            return Ok(games);
        }

        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] Game game)
        {
            var result = await this.gamesService.AddAsync(game);
            return Ok(result);
        }

        [HttpPut("{id}/tier")]
        public async Task<IActionResult> UpdateTier(int id, [FromBody] string? Tier)
        {
            var IsSuccessfull = await gamesService.UpdateTierAsync(id, Tier);

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
