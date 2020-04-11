namespace PugnaFighting.Services.Data.Cutmen
{
    using System.Collections.Generic;
    using System.Linq;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Mapping;

    public class CutmenService : ICutmenService
    {
        private readonly IDeletableEntityRepository<Cutman> cutmenService;

        public CutmenService(IDeletableEntityRepository<Cutman> cutmenService)
        {
            this.cutmenService = cutmenService;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Cutman> query =
                this.cutmenService.All().OrderBy(x => x.Price);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
