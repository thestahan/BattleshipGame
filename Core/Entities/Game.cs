namespace Core.Entities
{
    public class Game
    {
        public Game(string playerOneName, string playerTwoName)
        {
            PlayerOne = new Player
            {
                Name = playerOneName
            };

            PlayerTwo = new Player
            {
                Name = playerTwoName
            };
        }

        public int Id { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public static char MinLetter { get; } = 'A';
        public static char MaxLetter { get; } = 'J';
        public static int MinNumber { get; } = 1;
        public static int MaxNumber { get; } = 10;
    }
}
