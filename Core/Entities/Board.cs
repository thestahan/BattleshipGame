using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Board
    {
        public Guid Id { get; set; }
        public List<Shot> Shots { get; set; } = new List<Shot>();
        public List<Ship> Ships { get; set; } = new List<Ship>();
    }
}
