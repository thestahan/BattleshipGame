using System;

namespace Core.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Board SelfBoard { get; set; }
        public Board EnemyBoard { get; set; }
    }
}
