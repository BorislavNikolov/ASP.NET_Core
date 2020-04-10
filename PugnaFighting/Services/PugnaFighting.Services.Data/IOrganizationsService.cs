namespace PugnaFighting.Services.Data
{
    using System.Collections.Generic;

    public interface IOrganizationsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
