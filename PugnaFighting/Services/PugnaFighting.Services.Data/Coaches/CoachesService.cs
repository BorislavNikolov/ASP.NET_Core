namespace PugnaFighting.Services.Data.Coaches
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.ViewModels.Coaches;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<Coach> coachesRepository;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;
        private readonly ISkillsService skillsService;

        public CoachesService(
            IDeletableEntityRepository<Coach> coachesRepository,
            IDeletableEntityRepository<Fighter> fightersRepository,
            ISkillsService skillsService)
        {
            this.coachesRepository = coachesRepository;
            this.fightersRepository = fightersRepository;
            this.skillsService = skillsService;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Coach> query =
                this.coachesRepository.All().OrderBy(x => x.Price);
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

        public async Task AppointCoachToFighterAsync(Fighter fighter, int coachId)
        {
            var coach = this.GetById<DetailsCoachViewModel>(coachId);
            fighter.Skill = this.skillsService.GetById(fighter.SkillId);

            fighter.CoachId = coachId;
            fighter.Skill.Striking = fighter.Skill.Striking + coach.SkillBonus >= 100 ? 100 : fighter.Skill.Striking + coach.SkillBonus;
            fighter.Skill.Grappling = fighter.Skill.Grappling + coach.SkillBonus >= 100 ? 100 : fighter.Skill.Grappling + coach.SkillBonus;
            fighter.Skill.Wrestling = fighter.Skill.Wrestling + coach.SkillBonus >= 100 ? 100 : fighter.Skill.Wrestling + coach.SkillBonus;
            fighter.Skill.Strenght = fighter.Skill.Strenght + coach.SkillBonus >= 100 ? 100 : fighter.Skill.Strenght + coach.SkillBonus;
            fighter.Skill.Stamina = fighter.Skill.Stamina + coach.SkillBonus >= 100 ? 100 : fighter.Skill.Stamina + coach.SkillBonus;

            await this.fightersRepository.SaveChangesAsync();
        }

        public async Task FireCoachAsync(Fighter fighter)
        {
            var coach = this.GetById<DetailsCoachViewModel>(int.Parse(fighter.CoachId.ToString()));
            fighter.Skill = this.skillsService.GetById(fighter.SkillId);

            fighter.CoachId = null;
            fighter.Coach = null;

            fighter.Skill.Striking -= coach.SkillBonus;
            fighter.Skill.Grappling -= coach.SkillBonus;
            fighter.Skill.Wrestling -= coach.SkillBonus;
            fighter.Skill.Strenght -= coach.SkillBonus;
            fighter.Skill.Stamina -= coach.SkillBonus;

            await this.fightersRepository.SaveChangesAsync();
        }
    }
}
