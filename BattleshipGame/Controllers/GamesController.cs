using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("InitGame")]
        public ActionResult InitGame()
        {
            var game = _gameService.InitGame();

            return Ok(game);
        }

        [HttpPatch]
        public IActionResult ShotEnemy()
        {
            return View();
        }
    }
}
