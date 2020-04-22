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
        private readonly IBiographiesService biographiesService;
        private readonly ISkillsService skillsService;

        public FightersService(
            IDeletableEntityRepository<Fighter> fightersRepository,
            IDeletableEntityRepository<Record> recordsRepository,
            IDeletableEntityRepository<Fight> fightsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IBiographiesService biographiesService,
            ISkillsService skillService)
        {
            this.fightersRepository = fightersRepository;
            this.recordsRepository = recordsRepository;
            this.fightsRepository = fightsRepository;
            this.biographiesService = biographiesService;
            this.usersRepository = usersRepository;
            this.skillsService = skillService;
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

        public async Task<Fight> FightAsync(Fighter fighter, Fighter opponet, ApplicationUser user)
        {
            fighter.Skill = this.skillsService.GetById(fighter.SkillId);
            opponet.Skill = this.skillsService.GetById(opponet.SkillId);
            opponet.Biography = this.biographiesService.GetById(opponet.BiographyId);

            var fighterOverall = this.skillsService.GetSkillPointsOverall(fighter.Skill);
            var opponentOverall = this.skillsService.GetSkillPointsOverall(opponet.Skill);

            var result = string.Empty;
            var method = string.Empty;

            if (fighterOverall > opponentOverall)
            {
                method = fighter.Skill.Striking > opponet.Skill.Striking ? "KO/TKO" :
                         fighter.Skill.Grappling > opponet.Skill.Grappling ? "Submission" : "Decision";
                result = "Win";
                user.Coins += fighter.MoneyPerFight + (fighter.FansCount * 10);
                fighter.FansCount += (int)Math.Ceiling(opponet.FansCount * 0.05);
            }
            else if (fighterOverall < opponentOverall)
            {
                method = fighter.Skill.Striking < opponet.Skill.Striking ? "KO/TKO" :
                         fighter.Skill.Grappling < opponet.Skill.Grappling ? "Submission" : "Decision";
                result = "Lose";
                user.Coins += fighter.MoneyPerFight;
                fighter.FansCount -= (int)Math.Floor(opponet.FansCount * 0.05);
            }
            else
            {
                method = "Decision";
                result = "Draw";
                user.Coins += fighter.MoneyPerFight + (fighter.FansCount * 5);
            }

            var opponentName = opponet.Biography.FirstName + " " + opponet.Biography.LastName;

            var fightId = await this.MakeFightAsync(opponentName, result, method);
            var fight = this.fightsRepository.All().Where(x => x.Id == fightId).FirstOrDefault();

            return fight;
        }

        public async Task AddFightToRecordAsync(Fight fight, Fighter fighter)
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

            record.Fights.ToList().Append(fight);

            await this.recordsRepository.SaveChangesAsync();
        }

        public Record GetRecordById(int id)
        {
            var record = this.recordsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            return record;
        }

        private async Task<int> MakeFightAsync(string opponentName, string result, string method)
        {
            var fight = new Fight
            {
                DateTime = DateTime.UtcNow,
                Method = method,
                OpponentName = opponentName,
                Result = result,
            };

            await this.fightsRepository.AddAsync(fight);
            await this.fightsRepository.SaveChangesAsync();

            return fight.Id;
        }
    }
}
