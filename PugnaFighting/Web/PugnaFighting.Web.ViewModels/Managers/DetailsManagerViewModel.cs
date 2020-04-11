namespace PugnaFighting.Web.ViewModels.Managers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class DetailsManagerViewModel : IMapFrom<Manager>
    {
        [Required]
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
        public int MoneyPerFight { get; set; }

        [Required]
        public int FansCount { get; set; }
    }
}
