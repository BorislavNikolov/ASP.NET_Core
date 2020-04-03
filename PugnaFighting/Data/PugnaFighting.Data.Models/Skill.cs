namespace PugnaFighting.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Skill : BaseDeletableModel<int>
    {
        [Required]
        [Range(0, 100)]
        public int Striking { get; set; }

        [Required]
        [Range(0, 100)]
        public int Grappling { get; set; }

        [Required]
        [Range(0, 100)]
        public int Wrestling { get; set; }

        [Required]
        [Range(0, 100)]
        public int Health { get; set; }

        [Required]
        [Range(0, 100)]
        public int Strenght { get; set; }

        [Required]
        [Range(0, 100)]
        public int Stamina { get; set; }
    }
}
