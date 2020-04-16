namespace PugnaFighting.Web.ViewModels.Managers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.Infrastructure;
    using PugnaFighting.Web.ViewModels.Fighters;

    public class CreateManagerViewModel : IMapTo<Manager>
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
        [Range(700, 3000)]
        public int MoneyPerFight { get; set; }

        [Required]
        [Range(100, 500)]
        public int FansCount { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png" })]
        public IFormFile Picture { get; set; }

        [Range(1, int.MaxValue)]
        public int FighterId { get; set; }

        public IEnumerable<FightersDropDownViewModel> Fighters { get; set; }
    }
}
