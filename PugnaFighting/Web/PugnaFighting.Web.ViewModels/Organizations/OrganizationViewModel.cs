namespace PugnaFighting.Web.ViewModels.Organizations
{
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class OrganizationViewModel : IMapFrom<Organization>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        [Required]
        public int FansCount { get; set; }

        [Required]
        public int InstantCash { get; set; }

        [Required]
        public int MoneyPerFight { get; set; }
    }
}
