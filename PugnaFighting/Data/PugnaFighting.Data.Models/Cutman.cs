namespace PugnaFighting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Cutman : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nationality { get; set; }

        [Required]
        [Range(18, 70)]
        public int Age { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
