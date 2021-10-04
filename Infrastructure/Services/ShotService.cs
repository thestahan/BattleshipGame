using Core.Entities;
using Core.Interfaces;
using System.Linq;

namespace Infrastructure.Services
{
    public class ShotService : IShotService
    {
        public Shot MakeShot(Game game, string position)
        {
            bool playerOneMakingMove = game.NextTurnPlayerId == game.PlayerOne.Id;

            if (playerOneMakingMove)
            {
                return GetPlayerShot(game.PlayerOne.SelfBoard, game.PlayerOne.EnemyBoard, game.PlayerOne, position);
            }

            return GetPlayerShot(game.PlayerTwo.SelfBoard, game.PlayerTwo.EnemyBoard, game.PlayerTwo, position);
        }

        public bool ShipGotSank(Board enemyBoard, string position)
        {
            var ship = enemyBoard.Ships.FirstOrDefault(x => x.CellsPositions.Contains(position));

            if (ship == null) return false;

            return ship.RemainingHealth == 0;
        }

        private static Shot GetPlayerShot(Board selfBoard, Board enemyBoard, Player player, string position)
        {
            var ship = enemyBoard.Ships.FirstOrDefault(x => x.CellsPositions.Contains(position));

            var shot = new Shot
            {
                Position = position,
                ShipWasHit = ship != null,
                ShotByPlayer = player
            };

            if (ship != null)
            {
                ship.RemainingHealth--;
            }

            selfBoard.Shots.Add(shot);

            enemyBoard.Shots.Add(shot.Clone());

            return shot;
        }
    }
}
