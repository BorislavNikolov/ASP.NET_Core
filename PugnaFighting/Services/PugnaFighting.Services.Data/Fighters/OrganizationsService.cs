namespace PugnaFighting.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class OrganizationsService : IOrganizationsService
    {
        private readonly IDeletableEntityRepository<Organization> organizationsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;

        public OrganizationsService(
            IDeletableEntityRepository<Organization> organizationsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Fighter> fightersRepository)
        {
            this.organizationsRepository = organizationsRepository;
            this.usersRepository = usersRepository;
            this.fightersRepository = fightersRepository;
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

        public Organization GetById(int id)
        {
            var organization = this.organizationsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            return organization;
        }

        public async Task SetOrganizationAsync(Fighter fighter, int organizationId, ApplicationUser user)
        {
            var organization = this.GetById(organizationId);

            fighter.OrganizationId = organizationId;
            fighter.MoneyPerFight = organization.MoneyPerFight;
            fighter.FansCount += organization.FansCount;
            user.Coins += organization.InstantCash;

            await this.usersRepository.SaveChangesAsync();
            await this.fightersRepository.SaveChangesAsync();
        }
    }
}
