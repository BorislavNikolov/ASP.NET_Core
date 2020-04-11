namespace PugnaFighting.Services.Data.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Mapping;

    public class ManagersService : IManagersService
    {
        private readonly IDeletableEntityRepository<Manager> managersRepository;

        public ManagersService(IDeletableEntityRepository<Manager> managersRepository)
        {
            this.managersRepository = managersRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Manager> query =
                this.managersRepository.All().Where(x => x.IsCustom == false).OrderBy(x => x.Price);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var manager = this.managersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return manager;
        }
    }
}
