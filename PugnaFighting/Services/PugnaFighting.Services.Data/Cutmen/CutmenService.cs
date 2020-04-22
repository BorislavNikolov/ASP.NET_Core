namespace PugnaFighting.Services.Data.Cutmen
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.ViewModels.Cutmen;

    public class CutmenService : ICutmenService
    {
        private readonly IDeletableEntityRepository<Cutman> cutmenRepository;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;
        private readonly ISkillsService skillsService;

        public CutmenService(
            IDeletableEntityRepository<Cutman> cutmenRepository,
            IDeletableEntityRepository<Fighter> fightersRepository,
            ISkillsService skillsService)
        {
            this.cutmenRepository = cutmenRepository;
            this.fightersRepository = fightersRepository;
            this.skillsService = skillsService;
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
            var coach = this.cutmenRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return coach;
        }

        public async Task AppointCutmanToFighterAsync(Fighter fighter, int cutmanId)
        {
            var cutman = this.GetById<DetailsCutmanViewModel>(cutmanId);
            fighter.Skill = this.skillsService.GetById(fighter.SkillId);

            fighter.CutmanId = cutmanId;
            fighter.Skill.Health = fighter.Skill.Health + cutman.HealthBonus >= 100 ? 100 : fighter.Skill.Health + cutman.HealthBonus;

            await this.fightersRepository.SaveChangesAsync();
        }

        public async Task FireCutmanAsync(Fighter fighter)
        {
            var cutman = this.GetById<DetailsCutmanViewModel>(int.Parse(fighter.CutmanId.ToString()));
            fighter.Skill = this.skillsService.GetById(fighter.SkillId);

            fighter.CutmanId = null;
            fighter.Cutman = null;
            fighter.Skill.Health -= cutman.HealthBonus;

            await this.fightersRepository.SaveChangesAsync();
        }
    }
}
