using Core.Interfaces;
using System;

namespace Core.Entities
{
    public class Shot : ICloneable<Shot>
    {
        public Guid Id { get; set; }
        public Player ShotByPlayer { get; set; }
        public Guid ShotByPlayerId { get; set; }
        public string Position { get; set; }
        public bool ShipWasHit { get; set; }

        public Shot Clone()
        {
            return new Shot
            {
                Position = Position,
                ShotByPlayer = ShotByPlayer,
                ShipWasHit = ShipWasHit
            };
        }
    }
}
