namespace PugnaFighting.Services.Data
{
    using System;
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
        private readonly IDeletableEntityRepository<Fight> fightsRepository;
        private readonly IDeletableEntityRepository<Record> recordsRepository;
        private readonly IDeletableEntityRepository<Skill> skillsRepository;
        private readonly IDeletableEntityRepository<Biography> biographiesRepository;
        private readonly IOrganizationsService organizationsService;

        public FightersService(
            IDeletableEntityRepository<Fighter> fightersRepository,
            IOrganizationsService organizationsService,
            IDeletableEntityRepository<Record> recordsRepository,
            IDeletableEntityRepository<Skill> skillsRepository,
            IDeletableEntityRepository<Fight> fightsRepository,
            IDeletableEntityRepository<Biography> biographiesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.fightersRepository = fightersRepository;
            this.organizationsService = organizationsService;
            this.recordsRepository = recordsRepository;
            this.skillsRepository = skillsRepository;
            this.fightsRepository = fightsRepository;
            this.biographiesRepository = biographiesRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<int> CreateAsync(int skillId, int biogrphyId, int recordId, int categoryId, ApplicationUser user)
        {
            var fighter = new Fighter
            {
                CategoryId = categoryId,
                UserId = user.Id,
                SkillId = biogrphyId,
                BiographyId = skillId,
                RecordId = recordId,
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

        public T GetById<T>(int id)
        {
            var fighter = this.fightersRepository.All().Where(x => x.Id == id)
               .To<T>().FirstOrDefault();
            return fighter;
        }

        public IEnumerable<T> GetAllFightersWithoutManagers<T>(string userId)
        {
            IQueryable<Fighter> query =
               this.fightersRepository.All().Where(x => x.ManagerId == null && x.UserId == userId);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllFightersWithoutCoaches<T>(string userId)
        {
            IQueryable<Fighter> query =
               this.fightersRepository.All().Where(x => x.CoachId == null && x.UserId == userId);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllFightersWithoutCutmen<T>(string userId)
        {
            IQueryable<Fighter> query =
               this.fightersRepository.All().Where(x => x.CutmanId == null && x.UserId == userId);

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

        public async Task<Fight> Fight(Fighter fighter, Fighter opponet)
        {
            fighter.Skill = this.skillsRepository.All().Where(x => x.Id == fighter.SkillId).FirstOrDefault();
            opponet.Skill = this.skillsRepository.All().Where(x => x.Id == opponet.SkillId).FirstOrDefault();

            opponet.Biography = this.biographiesRepository.All().Where(x => x.Id == opponet.BiographyId).FirstOrDefault();

            var fighterOverall = fighter.Skill.Striking +
                                fighter.Skill.Grappling +
                                fighter.Skill.Wrestling +
                                fighter.Skill.Striking +
                                fighter.Skill.Stamina +
                                fighter.Skill.Strenght;

            var opponentOverall = opponet.Skill.Striking +
                                opponet.Skill.Grappling +
                                opponet.Skill.Wrestling +
                                opponet.Skill.Striking +
                                opponet.Skill.Stamina +
                                opponet.Skill.Strenght;

            var result = string.Empty;

            if (fighterOverall > opponentOverall)
            {
                result = "Win";
            }
            else if (fighterOverall > opponentOverall)
            {
                result = "Lose";
            }
            else
            {
                result = "Draw";
            }

            var opponentName = opponet.Biography.FirstName + " " + opponet.Biography.LastName;

            var fightId = await this.MakeFight(opponentName, result);
            var fight = this.fightsRepository.All().Where(x => x.Id == fightId).FirstOrDefault();

            return fight;
        }

        public async Task<int> MakeFight(string opponentName, string result)
        {
            var fight = new Fight
            {
                DateTime = DateTime.UtcNow,
                Method = "KO",
                OpponentName = opponentName,
                Result = result,
            };

            await this.fightsRepository.AddAsync(fight);
            await this.fightsRepository.SaveChangesAsync();

            return fight.Id;
        }

        public async Task AddFightToRecord(Fight fight, Fighter fighter)
        {
            var record = this.GetRecordById(fighter.RecordId);

            if (fight.Result == "Win")
            {
                record.Wins++;
            }
            else if (fight.Result == "Draw")
            {
                record.Draws++;
            }
            else
            {
                record.Losses++;
            }

            if (record.Fights == null)
            {
                record.Fights = new List<Fight>();
            }

            record.Fights.ToList().Add(fight);

            await this.recordsRepository.SaveChangesAsync();
        }

        public Record GetRecordById(int id)
        {
            var record = this.recordsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            return record;
        }
    }
}
