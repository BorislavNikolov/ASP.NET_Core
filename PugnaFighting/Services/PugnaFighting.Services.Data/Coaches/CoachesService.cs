namespace PugnaFighting.Services.Data.Coaches
{
    using System.Collections.Generic;
    using System.Linq;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Mapping;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<Coach> coachesRepository;

        public CoachesService(IDeletableEntityRepository<Coach> coachesRepository)
        {
            this.coachesRepository = coachesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Coach> query =
                this.coachesRepository.All().Where(x => x.IsCustom == false).OrderBy(x => x.Price);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var coach = this.coachesRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return coach;
        }
    }
}
