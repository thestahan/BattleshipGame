using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Ship : ICloneable<Ship>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> CellsPositions { get; set; }
        public int Size { get; set; }
        public int RemainingHealth { get; set; }

        public Ship Clone()
        {
            return new Ship
            {
                Name = Name,
                CellsPositions = CellsPositions,
                Size = Size,
                RemainingHealth = RemainingHealth
            };
        }
    }
}
