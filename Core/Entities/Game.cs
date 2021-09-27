namespace Core.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
    }
}
