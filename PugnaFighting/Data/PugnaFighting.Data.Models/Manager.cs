namespace PugnaFighting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Manager : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string BornCountry { get; set; }

        [Required]
        [Range(18, 90)]
        public int Age { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int MoneyPerFight { get; set; }

        [Required]
        public int FansCount { get; set; }
    }
}
