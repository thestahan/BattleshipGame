using System;

namespace API.Dtos
{
    public class ShotReturnDto
    {
        public string Position { get; set; }
        public bool Hit { get; set; }
        public bool Sank { get; set; }
        public Guid NextPlayerId { get; set; }
        public bool GameFinished { get; set; }
    }
}
