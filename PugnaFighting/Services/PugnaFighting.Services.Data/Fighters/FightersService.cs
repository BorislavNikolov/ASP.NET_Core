namespace PugnaFighting.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class FightersService : IFightersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;
        private readonly IOrganizationsService organizationsService;

        public FightersService(
            IDeletableEntityRepository<Fighter> fightersRepository,
            IOrganizationsService organizationsService,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.fightersRepository = fightersRepository;
            this.organizationsService = organizationsService;
            this.usersRepository = usersRepository;
        }

        public async Task<int> CreateAsync(int skillId, int biogrphyId, int categoryId, ApplicationUser user)
        {
            var fighter = new Fighter
            {
                CategoryId = categoryId,
                UserId = user.Id,
                SkillId = biogrphyId,
                BiographyId = skillId,
                FansCount = 100,
            };

            user.FightersCount++;

            await this.usersRepository.SaveChangesAsync();
            await this.fightersRepository.AddAsync(fighter);
            await this.fightersRepository.SaveChangesAsync();

            return fighter.Id;
        }

        public Fighter GetById(int id)
        {
            var fighter = this.fightersRepository.All().Where(x => x.Id == id).FirstOrDefault();
            return fighter;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Fighter> query =
               this.fightersRepository.All().Where(x => x.ManagerId == null);

            return query.To<T>().ToList();
        }

        public async Task SetOrganization(int fighterId, int organizationId, ApplicationUser user)
        {
            var fighter = this.GetById(fighterId);
            var organization = this.organizationsService.GetById(organizationId);

            fighter.OrganizationId = organizationId;
            fighter.MoneyPerFight = organization.MoneyPerFight;
            fighter.FansCount += organization.FansCount;
            user.Coins += organization.InstantCash;

            await this.usersRepository.SaveChangesAsync();
            await this.fightersRepository.SaveChangesAsync();
        }

        public T GetBestStriker<T>(string organizationName)
        {
            var fighter = this.fightersRepository.All()
                .Where(x => x.Organization.Name.ToLower() == organizationName.ToLower())
                .OrderByDescending(x => x.Skill.Striking)
                .ThenByDescending(x => x.FansCount)
                .ThenBy(x => x.Biography.Age)
                .To<T>()
                .FirstOrDefault();

            return fighter;
        }

        public T GetBestWrestler<T>(string organizationName)
        {
            var fighter = this.fightersRepository.All()
                .Where(x => x.Organization.Name.ToLower() == organizationName.ToLower())
                .OrderByDescending(x => x.Skill.Wrestling)
                .ThenByDescending(x => x.FansCount)
                .ThenBy(x => x.Biography.Age)
                .To<T>()
                .FirstOrDefault();

            return fighter;
        }

        public T GetBestGrappler<T>(string organizationName)
        {
            var fighter = this.fightersRepository.All()
                .Where(x => x.Organization.Name.ToLower() == organizationName.ToLower())
                .OrderByDescending(x => x.Skill.Grappling)
                .ThenByDescending(x => x.FansCount)
                .ThenBy(x => x.Biography.Age)
                .To<T>()
                .FirstOrDefault();

            return fighter;
        }

        public async Task AppointManagerToFighter(Fighter fighter, int managerId)
        {
            fighter.ManagerId = managerId;

            await this.fightersRepository.SaveChangesAsync();
        }
    }
}
