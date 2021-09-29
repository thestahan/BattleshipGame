using Core.Entities;
using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Interfaces
{
    public class GameService : IGameService
    {
        private readonly char _minLetter = 'A';
        private readonly char _maxLetter = 'J';
        private readonly int _minNumber = 1;
        private readonly int _maxNumber = 10;

        public Game InitGame()
        {
            var game = new Game("Player one", "Player two");

            game = SetInitialShips(game);

            return game;
        }

        private Game SetInitialShips(Game game)
        {
            var playerOneShips = GenerateRandomlyPlacedShips();
            var playerTwoShips = GenerateRandomlyPlacedShips();

            game.PlayerOne.SelfBoard.Ships = playerOneShips;
            game.PlayerOne.EnemyBoard.Ships = playerTwoShips;

            game.PlayerTwo.SelfBoard.Ships = playerTwoShips;
            game.PlayerTwo.EnemyBoard.Ships = playerOneShips;

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

                        if (PointsInDirectionExceedMap(shipSize, currentDirection, point)) continue;

                        if (PointsInDirectionOverlapShip(shipSize, currentDirection, point, ships)) continue;

                        coords = GetShipCoords(shipSize, currentDirection, point);

                        ships.Add(new Ship
                        {
                            CellsPositions = coords,
                            Name = "",
                            Size = shipSize
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
                Letter = randomizer.GetRandomLetterFromRange(_minLetter, _maxLetter),
                Number = randomizer.GetRandomNumberFromRange(_minNumber, _maxNumber)
            };

        private bool PointsInDirectionExceedMap(int shipSize, Direction direction, Point startingPoint)
        {
            if (direction == Direction.TOP)
            {
                char letterToCheck = (char)(startingPoint.Letter - shipSize);
                return LetterExceededsRange(letterToCheck);
            }
            else if (direction == Direction.LEFT)
            {
                return NumberExceededsRange(startingPoint.Number - shipSize);
            }
            else if (direction == Direction.BOTTOM)
            {
                char letterToCheck = (char)(startingPoint.Letter + shipSize);
                return LetterExceededsRange(letterToCheck);
            }
            else if (direction == Direction.RIGHT)
            {
                return NumberExceededsRange(startingPoint.Number + shipSize);
            }

            return true;
        }

        private bool PointsInDirectionOverlapShip(int shipSize, Direction direction, Point startingPoint, List<Ship> ships)
        {
            if (direction == Direction.TOP)
            {
                for (int i = 1; i < shipSize; i++)
                {
                    bool pointIsTaken = ships.Any(x => x.CellsPositions.Contains($"{(char)(startingPoint.Letter - i)}{startingPoint.Number}"));

                    if (pointIsTaken) return true;
                }

                return false;
            }
            else if (direction == Direction.LEFT)
            {
                for (int i = 1; i < shipSize; i++)
                {
                    bool pointIsTaken = ships.Any(x => x.CellsPositions.Contains($"{startingPoint.Letter}{startingPoint.Number - i}"));

                    if (pointIsTaken) return true;
                }

                return false;
            }
            else if (direction == Direction.BOTTOM)
            {
                for (int i = 1; i < shipSize; i++)
                {
                    bool pointIsTaken = ships.Any(x => x.CellsPositions.Contains($"{(char)(startingPoint.Letter + i)}{startingPoint.Number}"));

                    if (pointIsTaken) return true;
                }

                return false;
            }
            else if (direction == Direction.RIGHT)
            {
                for (int i = 1; i < shipSize; i++)
                {
                    bool pointIsTaken = ships.Any(x => x.CellsPositions.Contains($"{startingPoint.Letter}{startingPoint.Number + i}"));

                    if (pointIsTaken) return true;
                }

                return false;
            }

            return true;
        }

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

        private bool LetterExceededsRange(char letter) =>
            Convert.ToByte(letter) < Convert.ToByte(_minLetter) ||
            Convert.ToByte(letter) > Convert.ToByte(_maxLetter);

        private bool NumberExceededsRange(int number) =>
            number < _minNumber ||
            number > _maxNumber;
    }
}
