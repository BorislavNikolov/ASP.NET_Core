namespace PugnaFighting.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class PersonalInfo : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nationality { get; set; }

        [Required]
        [Range(18, 50)]
        public int Age { get; set; }

        [Required]
        public string PictureUrl { get; set; }
    }
}
