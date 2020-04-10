namespace PugnaFighting.Web.ViewModels.Organizations
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class OrganizationDropDownViewModel : IMapFrom<Organization>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
