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

        public IEnumerable<T> GetAllFightersWithoutManagers<T>()
        {
            IQueryable<Fighter> query =
               this.fightersRepository.All().Where(x => x.ManagerId == null);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllFightersWithoutCoaches<T>()
        {
            IQueryable<Fighter> query =
               this.fightersRepository.All().Where(x => x.CoachId == null);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllFightersWithoutCutmen<T>()
        {
            IQueryable<Fighter> query =
               this.fightersRepository.All().Where(x => x.CutmanId == null);

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

        public async Task FireManager(Fighter fighter)
        {
            fighter.ManagerId = null;
            fighter.Manager = null;

            await this.fightersRepository.SaveChangesAsync();
        }

        public async Task AppointCoachToFighter(Fighter fighter, int coachId)
        {
            fighter.CoachId = coachId;

            await this.fightersRepository.SaveChangesAsync();
        }

        public async Task FireCoach(Fighter fighter)
        {
            fighter.CoachId = null;
            fighter.Coach = null;

            await this.fightersRepository.SaveChangesAsync();
        }

        public async Task AppointCutmanToFighter(Fighter fighter, int cutmanId)
        {
            fighter.CutmanId = cutmanId;

            await this.fightersRepository.SaveChangesAsync();
        }

        public async Task FireCutman(Fighter fighter)
        {
            fighter.CutmanId = null;
            fighter.Cutman = null;

            await this.fightersRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllOpponents<T>(string userId, int? take = null, int skip = 0)
        {
            IQueryable<Fighter> query =
               this.fightersRepository.All()
               .Where(x => x.UserId != userId)
               .OrderByDescending(x => x.FansCount)
               .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetOpponentsCount(string userId)
        {
            return this.fightersRepository.All().Count(x => x.UserId != userId);
        }
    }
}
