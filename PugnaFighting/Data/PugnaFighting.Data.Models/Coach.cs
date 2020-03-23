﻿namespace PugnaFighting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Coach : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nationality { get; set; }

        [Required]
        [Range(18, 90)]
        public int Age { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
