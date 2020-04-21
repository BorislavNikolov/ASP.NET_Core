namespace PugnaFighting.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface IOrganizationsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Organization GetById(int id);

        Task SetOrganization(Fighter fighter, int organizationId, ApplicationUser user);
    }
}
