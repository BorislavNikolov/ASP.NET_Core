namespace PugnaFighting.Web.ViewModels.Coaches
{
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class CoachViewModel : IMapFrom<Coach>
    {
        [Required]
        public int Id { get; set; }

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
