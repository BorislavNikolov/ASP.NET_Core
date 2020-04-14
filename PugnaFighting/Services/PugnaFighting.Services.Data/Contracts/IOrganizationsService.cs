namespace PugnaFighting.Services.Data
{
    using System.Collections.Generic;

    using PugnaFighting.Data.Models;

    public interface IOrganizationsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        public Organization GetById(int id);
    }
}
