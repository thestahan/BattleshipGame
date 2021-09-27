namespace Core.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public Board SelfBoard { get; set; }
        public Board EnemyBoard { get; set; }
    }
}
