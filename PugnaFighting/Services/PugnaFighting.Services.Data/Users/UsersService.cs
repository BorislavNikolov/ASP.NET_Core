namespace PugnaFighting.Services.Data.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;

        public UsersService(IDeletableEntityRepository<Fighter> fightersRepository)
        {
            this.fightersRepository = fightersRepository;
        }

        public IEnumerable<T> GetAllFighters<T>(string userId)
        {
            IQueryable<Fighter> query =
                this.fightersRepository.All().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn);

            return query.To<T>().ToList();
        }

        public async Task<T> GetFighterById<T>(int id)
        {
            var fighter = await this.fightersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return fighter;
        }

        public async Task DeleteFighter(Fighter fighter)
        {
            fighter.IsDeleted = true;
            fighter.DeletedOn = DateTime.UtcNow;

            await this.fightersRepository.SaveChangesAsync();
        }
    }
}
