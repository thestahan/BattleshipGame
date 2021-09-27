using System.Collections.Generic;

namespace Core.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public IReadOnlyList<Shot> Shots { get; set; }
        public IReadOnlyList<Ship> Ships { get; set; }
    }
}
