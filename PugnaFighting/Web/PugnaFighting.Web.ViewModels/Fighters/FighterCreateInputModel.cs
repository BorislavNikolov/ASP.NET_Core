namespace PugnaFighting.Web.ViewModels.Fighters
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.Infrastructure;

    public class FighterCreateInputModel : IMapTo<Fighter>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        public string BornCountry { get; set; }

        [Required]
        public int Age { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png" })]
        public IFormFile Picture { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
