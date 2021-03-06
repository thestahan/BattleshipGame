using System;

namespace Core.Entities
{
    public class Game
    {
        public Game()
        {
        }

        public Game(string playerOneName, string playerTwoName)
        {
            PlayerOne = new Player
            {
                Name = playerOneName,
                SelfBoard = new Board(),
                EnemyBoard = new Board()
            };

            PlayerTwo = new Player
            {
                Name = playerTwoName,
                SelfBoard = new Board(),
                EnemyBoard = new Board()
            };

            NextTurnPlayer = PlayerOne;
        }

        public Guid Id { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Player NextTurnPlayer { get; set; }
        public Guid NextTurnPlayerId { get; set; }
        public bool Finished { get; set; }
        public static char MinLetter { get; } = 'A';
        public static char MaxLetter { get; } = 'J';
        public static int MinNumber { get; } = 1;
        public static int MaxNumber { get; } = 10;
    }
}
