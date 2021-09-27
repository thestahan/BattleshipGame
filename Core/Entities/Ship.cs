using System.Collections.Generic;

namespace Core.Entities
{
    public class Ship
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyList<string> CellsPositions { get; set; }
        public int Size { get; set; }
    }
}
