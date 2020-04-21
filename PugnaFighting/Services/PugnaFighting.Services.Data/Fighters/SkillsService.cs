namespace PugnaFighting.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.ViewModels.Skills;

    public class SkillsService : ISkillsService
    {
        private readonly IDeletableEntityRepository<Skill> skillsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public SkillsService(IDeletableEntityRepository<Skill> skillsRepository, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.skillsRepository = skillsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<int> CreateAsync()
        {
            var skill = new Skill
            {
                Striking = 65,
                Grappling = 65,
                Wrestling = 65,
                Stamina = 65,
                Health = 65,
                Strenght = 65,
            };

            await this.skillsRepository.AddAsync(skill);
            await this.skillsRepository.SaveChangesAsync();

            return skill.Id;
        }

        public Skill GetById(int id)
        {
            var skill = this.skillsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            return skill;
        }

        public T GetById<T>(int id)
        {
            var query = this.skillsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return query;
        }

        public async Task Delete(int id)
        {
            var skill = this.GetById(id);

            skill.IsDeleted = true;
            skill.DeletedOn = DateTime.UtcNow;

            await this.skillsRepository.SaveChangesAsync();
        }

        public async Task UpdateSkillPoints(TrainViewModel trainViewModel, int skillId)
        {
            var skill = this.GetById(skillId);

            skill.Grappling = trainViewModel.Grappling;
            skill.Striking = trainViewModel.Striking;
            skill.Wrestling = trainViewModel.Wrestling;
            skill.Stamina = trainViewModel.Stamina;
            skill.Health = trainViewModel.Health;
            skill.Strenght = trainViewModel.Strenght;

            await this.skillsRepository.SaveChangesAsync();
        }

        public int GetSkillPointsOverall(Skill skill)
        {
            var overall = skill.Striking + skill.Grappling + skill.Wrestling + skill.Strenght + skill.Stamina + skill.Health;
            return overall;
        }

        public bool ChechForEnoughCoinsToTrain(ApplicationUser user, Skill skill, TrainViewModel trainViewModel)
        {
            var overallSkillPointsAfterTraining = trainViewModel.Striking +
                                                  trainViewModel.Grappling +
                                                  trainViewModel.Wrestling +
                                                  trainViewModel.Stamina +
                                                  trainViewModel.Strenght +
                                                  trainViewModel.Health;

            var curentSkillPointsOverall = this.GetSkillPointsOverall(skill);
            var newSkillPoints = overallSkillPointsAfterTraining - curentSkillPointsOverall;

            if (newSkillPoints * 1000 > user.Coins)
            {
                return false;
            }

            user.Coins -= newSkillPoints * 1000;

            return true;
        }
    }
}
