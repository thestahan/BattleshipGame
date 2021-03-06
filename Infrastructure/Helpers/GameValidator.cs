using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Helpers
{
    public class GameValidator
    {
        public static bool NumberExceededsRange(int number) =>
            number < Game.MinNumber ||
            number > Game.MaxNumber;

        public static bool LetterExceededsRange(char letter) =>
            Convert.ToByte(letter) < Convert.ToByte(Game.MinLetter) ||
            Convert.ToByte(letter) > Convert.ToByte(Game.MaxLetter);

        public static bool PointsInDirectionOverlapShip(int shipSize, Direction direction, Point startingPoint, List<Ship> ships)
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

        public static bool PointsInDirectionExceedMap(int shipSize, Direction direction, Point startingPoint)
        {
            if (direction == Direction.TOP)
            {
                char letterToCheck = (char)(startingPoint.Letter - shipSize);
                return GameValidator.LetterExceededsRange(letterToCheck);
            }
            else if (direction == Direction.LEFT)
            {
                return GameValidator.NumberExceededsRange(startingPoint.Number - shipSize);
            }
            else if (direction == Direction.BOTTOM)
            {
                char letterToCheck = (char)(startingPoint.Letter + shipSize);
                return GameValidator.LetterExceededsRange(letterToCheck);
            }
            else if (direction == Direction.RIGHT)
            {
                return GameValidator.NumberExceededsRange(startingPoint.Number + shipSize);
            }

            return true;
        }

        public static bool ShotIsUnique(Game game, string position) 
        {
            bool playerOneMakingMove = game.NextTurnPlayerId == game.PlayerOne.Id;

            if (playerOneMakingMove)
            {
                return !game.PlayerOne.EnemyBoard.Shots.Any(x => x.Position == position);
            }

            return !game.PlayerTwo.EnemyBoard.Shots.Any(x => x.Position == position);
        }
    }
}
