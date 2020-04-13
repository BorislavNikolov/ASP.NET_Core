namespace PugnaFighting.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;

    public class SkillsService : ISkillsService
    {
        private readonly IDeletableEntityRepository<Skill> skillsRepository;

        public SkillsService(IDeletableEntityRepository<Skill> skillsRepository)
        {
            this.skillsRepository = skillsRepository;
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

        public async Task Delete(int id)
        {
            var skill = this.GetById(id);

            skill.IsDeleted = true;
            skill.DeletedOn = DateTime.UtcNow;

            await this.skillsRepository.SaveChangesAsync();
        }
    }
}
