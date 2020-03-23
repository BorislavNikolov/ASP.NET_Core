﻿namespace PugnaFighting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Organization
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Location { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        public virtual ICollection<Fighter> Fighters => new List<Fighter>();
    }
}