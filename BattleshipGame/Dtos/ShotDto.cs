using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class ShotDto
    {
        [Required]
        public Guid GameId { get; set; }

        [Required]
        public Guid PlayerId { get; set; }

        [Required]
        public char PositionRow { get; set; }

        [Required]
        public int PositionColumn { get; set; }
    }
}
