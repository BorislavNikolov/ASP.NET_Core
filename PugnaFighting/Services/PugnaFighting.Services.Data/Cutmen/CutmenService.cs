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
        private readonly IDeletableEntityRepository<Cutman> cutmenRepository;

        public CutmenService(IDeletableEntityRepository<Cutman> cutmenRepository)
        {
            this.cutmenRepository = cutmenRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Cutman> query =
                this.cutmenRepository.All().Where(x => x.IsCustom == false).OrderBy(x => x.Price);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var post = this.cutmenRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return post;
        }
    }
}
