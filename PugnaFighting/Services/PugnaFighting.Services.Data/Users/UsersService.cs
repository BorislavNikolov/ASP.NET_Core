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
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(
            IDeletableEntityRepository<Fighter> fightersRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.fightersRepository = fightersRepository;
            this.usersRepository = usersRepository;
        }

        public IEnumerable<T> GetAllFighters<T>(string userId)
        {
            IQueryable<Fighter> query =
                this.fightersRepository.All().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn);

            return query.To<T>().ToList();
        }

        public async Task<T> GetFighterByIdAsync<T>(int id)
        {
            var fighter = await this.fightersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return fighter;
        }

        public async Task DeleteFighterAsync(Fighter fighter, ApplicationUser user)
        {
            user.FightersCount--;
            user.Coins += 10000;
            fighter.IsDeleted = true;
            fighter.DeletedOn = DateTime.UtcNow;

            await this.usersRepository.SaveChangesAsync();
            await this.fightersRepository.SaveChangesAsync();
        }

        public async Task PayForNewFighterAsync(ApplicationUser user)
        {
            user.Coins -= 10000;
            await this.usersRepository.SaveChangesAsync();
        }

        public async Task PayForNewTeamMemberAsync(ApplicationUser user, int price)
        {
            user.Coins -= price;
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
