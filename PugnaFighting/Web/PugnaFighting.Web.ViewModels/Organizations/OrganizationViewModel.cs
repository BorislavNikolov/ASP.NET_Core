namespace PugnaFighting.Web.ViewModels.Organizations
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class OrganizationViewModel : IMapFrom<Organization>
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string LogoUrl { get; set; }

        public int FansCount { get; set; }

        public int InstantCash { get; set; }

        public int MoneyPerFight { get; set; }
    }
}
