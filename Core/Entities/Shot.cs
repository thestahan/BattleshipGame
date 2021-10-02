using System;

namespace Core.Entities
{
    public class Shot
    {
        public Guid Id { get; set; }
        public string ShotByPlayerName { get; set; }
        public string Position { get; set; }
        public bool ShipWasHit { get; set; }
    }
}
