using Core.Entities;
using Infrastructure.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Core.Interfaces
{
    public class GameService : IGameService
    {
        public Game InitGame()
        {
            var game = new Game("Player one", "Player two");

            game = SetInitialShips(game);

            return game;
        }

        public bool GameHasFinished(Game game, bool playerOneMakingMove)
        {
            if (playerOneMakingMove)
            {
                return game.PlayerOne.EnemyBoard.Ships.All(x => x.RemainingHealth == 0);
            }

            return game.PlayerTwo.EnemyBoard.Ships.All(x => x.RemainingHealth == 0);
        }

        private Game SetInitialShips(Game game)
        {
            var playerOneSelfBoardShips = GenerateRandomlyPlacedShips();
            var playerTwoEnemyBoardShips = playerOneSelfBoardShips.Select(x => x.Clone()).ToList();

            var playerTwoSelfBoardShips = GenerateRandomlyPlacedShips();
            var playerOneEnemyBoardShips = playerTwoSelfBoardShips.Select(x => x.Clone()).ToList();

            game.PlayerOne.SelfBoard.Ships = playerOneSelfBoardShips;
            game.PlayerOne.EnemyBoard.Ships = playerTwoEnemyBoardShips;

            game.PlayerTwo.SelfBoard.Ships = playerTwoSelfBoardShips;
            game.PlayerTwo.EnemyBoard.Ships = playerOneEnemyBoardShips;

            return game;
        }

        private List<Ship> GenerateRandomlyPlacedShips()
        {
            var randomizer = new Randomizer();

            var ships = new List<Ship>();

            var shipSizes = new int[] { 5, 4, 3, 3, 2 };

            foreach (var shipSize in shipSizes)
            {
                bool shipCoordsSelected = false;

                var coords = new List<string>();

                while (!shipCoordsSelected)
                {
                    var point = GetRandomPoint(randomizer);

                    bool pointIsOccupiedByShip = ships.Any(x => x.CellsPositions.Contains($"{point.Letter}{point.Number}"));

                    if (pointIsOccupiedByShip) continue;

                    var directions = new List<Direction> { Direction.TOP, Direction.LEFT, Direction.BOTTOM, Direction.RIGHT };

                    for (int i = 0; i < 4; i++)
                    {
                        Direction currentDirection = (Direction)randomizer.GetRandomNumberFromRange(directions.Count);

                        directions = directions.Where(x => x != currentDirection).ToList();

                        if (GameValidator.PointsInDirectionExceedMap(shipSize, currentDirection, point)) continue;

                        if (GameValidator.PointsInDirectionOverlapShip(shipSize, currentDirection, point, ships)) continue;

                        coords = GetShipCoords(shipSize, currentDirection, point);

                        ships.Add(new Ship
                        {
                            CellsPositions = coords,
                            Name = "",
                            Size = shipSize,
                            RemainingHealth = shipSize
                        });

                        shipCoordsSelected = true;

                        break;
                    }
                }
            }

            return ships;
        }

        private Point GetRandomPoint(Randomizer randomizer) =>
            new()
            {
                Letter = randomizer.GetRandomLetterFromRange(Game.MinLetter, Game.MaxLetter),
                Number = randomizer.GetRandomNumberFromRange(Game.MinNumber, Game.MaxNumber)
            };

        private List<string> GetShipCoords(int shipSize, Direction direction, Point startingPoint)
        {
            var coords = new List<string> { $"{startingPoint.Letter}{startingPoint.Number}" };

            if (direction == Direction.TOP)
            {
                for (int i = 0; i < shipSize - 1; i++)
                {
                    startingPoint.Letter--;

                    coords.Add($"{startingPoint.Letter}{startingPoint.Number}");
                }
            }
            else if (direction == Direction.LEFT)
            {
                for (int i = 0; i < shipSize - 1; i++)
                {
                    startingPoint.Number--;

                    coords.Add($"{startingPoint.Letter}{startingPoint.Number}"); ;
                }
            }
            else if (direction == Direction.BOTTOM)
            {
                for (int i = 0; i < shipSize - 1; i++)
                {
                    startingPoint.Letter++;

                    coords.Add($"{startingPoint.Letter}{startingPoint.Number}");
                }
            }
            else if (direction == Direction.RIGHT)
            {
                for (int i = 0; i < shipSize - 1; i++)
                {
                    startingPoint.Number++;

                    coords.Add($"{startingPoint.Letter}{startingPoint.Number}");
                }
            }

            return coords;
        }
    }
}
