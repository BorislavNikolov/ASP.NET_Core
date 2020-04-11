namespace PugnaFighting.Web.ViewModels.Cutmen
{
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class CutmanViewModel : IMapFrom<Cutman>
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
    }
}
