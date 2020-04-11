namespace PugnaFighting.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class FightersService : IFightersService
    {
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;

        public FightersService(IDeletableEntityRepository<Fighter> fightersRepository)
        {
            this.fightersRepository = fightersRepository;
        }

        public async Task<int> CreateAsync(int skillId, int biogrphyId, int categoryId, string userId)
        {
            var fighter = new Fighter
            {
                CategoryId = categoryId,
                UserId = userId,
                SkillId = biogrphyId,
                BiographyId = skillId,
                FansCount = 100,
            };

            await this.fightersRepository.AddAsync(fighter);
            await this.fightersRepository.SaveChangesAsync();

            return fighter.Id;
        }

        public Fighter GetById(int id)
        {
            var fighter = this.fightersRepository.All().Where(x => x.Id == id).FirstOrDefault();
            return fighter;
        }

        public async Task ChooseOrganization(int fighterId, int organizationId)
        {
            var fighter = this.GetById(fighterId);

            fighter.OrganizationId = organizationId;

            await this.fightersRepository.SaveChangesAsync();
        }
    }
}
