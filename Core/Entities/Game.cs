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
    }
}
