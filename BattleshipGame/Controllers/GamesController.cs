using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IShotService _shotService;
        private readonly IGameRepository _gameRepo;

        public GamesController(IGameService gameService, IShotService shotService, IGameRepository gameRepo)
        {
            _gameService = gameService;
            _shotService = shotService;
            _gameRepo = gameRepo;
        }

        [HttpPost("InitGame")]
        public async Task<ActionResult<Game>> InitGame(CancellationToken cancellationToken)
        {
            var game = _gameService.InitGame();

            bool added = await _gameRepo.AddGameAsync(game, cancellationToken);

            if (!added) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(game);
        }

        [HttpPost("Shoot")]
        public async Task<ActionResult> Shoot(ShotDto dto, CancellationToken cancellationToken)
        {
            bool isPositionRowValid = !GameValidator.LetterExceededsRange(dto.PositionRow);

            if (!isPositionRowValid) return BadRequest("Position row exceeds range");

            bool isPositionColumnValid = !GameValidator.NumberExceededsRange(dto.PositionColumn);

            if (!isPositionColumnValid) return BadRequest("Position column exceeds range");

            var game = await _gameRepo.GetGameByIdAsync(dto.GameId, cancellationToken);

            if (game.Finished) return BadRequest("Given game has already been finished");

            bool correctPlayerMakingMove = game.NextTurnPlayerId == dto.PlayerId;

            if (!correctPlayerMakingMove) return BadRequest("Given player is not allowed to make a move now");

            bool shotIsUnique = GameValidator.ShotIsUnique(game, $"{dto.PositionRow}{dto.PositionColumn}");

            if (!shotIsUnique) return BadRequest("Given postiotion has already been shot");

            string position = $"{dto.PositionRow}{dto.PositionColumn}";

            var shot = _shotService.MakeShot(game, position);

            bool playerOneMakingMove = game.NextTurnPlayerId == game.PlayerOne.Id;

            if (playerOneMakingMove)
            {
                await _gameRepo.UpdateBoardAsync(game.PlayerOne.EnemyBoard, cancellationToken);
                await _gameRepo.UpdateBoardAsync(game.PlayerTwo.SelfBoard, cancellationToken);
            }
            else
            {
                await _gameRepo.UpdateBoardAsync(game.PlayerTwo.EnemyBoard, cancellationToken);
                await _gameRepo.UpdateBoardAsync(game.PlayerOne.SelfBoard, cancellationToken);
            }

            return Ok(new ShotReturnDto
            {
                Hit = shot.ShipWasHit,
                Position = shot.Position,
                Sank = playerOneMakingMove ? _shotService.ShipGotSank(game.PlayerOne.EnemyBoard, position) : 
                                             _shotService.ShipGotSank(game.PlayerTwo.EnemyBoard, position)
            });
        }
    }
}
