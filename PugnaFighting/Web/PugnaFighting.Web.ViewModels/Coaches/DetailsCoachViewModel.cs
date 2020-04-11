namespace PugnaFighting.Web.ViewModels.Coaches
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class DetailsCoachViewModel : IMapFrom<Coach>
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string BornCountry { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int SkillBonus { get; set; }
    }
}
