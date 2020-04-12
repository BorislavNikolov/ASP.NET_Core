namespace PugnaFighting.Services.Data
{
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface IFightersService
    {
        Task<int> CreateAsync(int skillId, int biographyId, int categoryId, string userId);

        Fighter GetById(int id);

        Task ChooseOrganization(int fighterId, int organizationId);

        public T GetBestStriker<T>(string organizationName);

        public T GetBestWrestler<T>(string organizationName);

        public T GetBestGrappler<T>(string organizationName);
    }
}
