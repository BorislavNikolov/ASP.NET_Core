namespace PugnaFighting.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class OrganizationsService : IOrganizationsService
    {
        private readonly IDeletableEntityRepository<Organization> organizationsRepository;

        public OrganizationsService(IDeletableEntityRepository<Organization> organizationsRepository)
        {
            this.organizationsRepository = organizationsRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Organization> query =
                this.organizationsRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
