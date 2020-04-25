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
        [MaxLength(50)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string LastName { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string BornCountry { get; set; }

        [Required]
        [Range(18, 50)]
        public int Age { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png" })]
        public IFormFile Picture { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
