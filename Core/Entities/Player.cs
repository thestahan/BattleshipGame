﻿namespace Core.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Board SelfBoard { get; set; }
        public Board EnemyBoard { get; set; }
    }
}
