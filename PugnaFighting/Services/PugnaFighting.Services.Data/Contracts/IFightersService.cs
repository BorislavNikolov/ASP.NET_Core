namespace PugnaFighting.Services.Data
{
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface IFightersService
    {
        Task<int> CreateAsync(int skillId, int biographyId, int categoryId, ApplicationUser user);

        Fighter GetById(int id);

        Task SetOrganization(int fighterId, int organizationId, ApplicationUser user);

        T GetBestStriker<T>(string organizationName);

        T GetBestWrestler<T>(string organizationName);

        T GetBestGrappler<T>(string organizationName);
    }
}
