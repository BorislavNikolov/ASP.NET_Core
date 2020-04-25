namespace PugnaFighting.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using PugnaFighting.Data;
    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Data.Repositories;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Data.Users;

    using Xunit;

    public class UsersServiceTests
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IUsersService usersService;

        public UsersServiceTests()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.applicationDbContext = new ApplicationDbContext(options.Options);
            this.fightersRepository = new EfDeletableEntityRepository<Fighter>(this.applicationDbContext);
            this.usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.applicationDbContext);
            this.usersService = new UsersService(this.fightersRepository, this.usersRepository);

            var fighter = new Fighter()
            {
                Id = 2,
                SkillId = 2,
                BiographyId = 2,
                OrganizationId = 2,
                CategoryId = 3,
                UserId = "51926c23-8a91-4e7e-94be-a97dd84bad1d",
                RecordId = 1,
                MoneyPerFight = 200,
                FansCount = 300,
            };

            this.fightersRepository.AddAsync(fighter);
            this.fightersRepository.SaveChangesAsync();

            var user = new ApplicationUser()
            {
                Id = "51926c23-8a91-4e7e-94be-a97dd84bad1d",
                Coins = 10000,
                UserName = "TheBestFighter",
                FightersCount = 3,
            };

            this.usersRepository.AddAsync(user);
            this.usersRepository.SaveChangesAsync();
        }

        [Fact]
        public async Task DeleteFighterAsync_ShouldDecreaseUserFightersCount()
        {
            const int ExpectedFightersCount = 2;
            var user = await this.usersRepository.All().FirstAsync();
            var fighter = await this.fightersRepository.All().FirstAsync();

            await this.usersService.DeleteFighterAsync(fighter, user);

            Assert.Equal(ExpectedFightersCount, user.FightersCount);
        }

        [Fact]
        public async Task DeleteFighterAsync_ShouldIncreaseUserCoins()
        {
            const int ExpectedCoins = 20000;
            var user = await this.usersRepository.All().FirstAsync();
            var fighter = await this.fightersRepository.All().FirstAsync();

            await this.usersService.DeleteFighterAsync(fighter, user);

            Assert.Equal(ExpectedCoins, user.Coins);
        }

        [Fact]
        public async Task DeleteFighterAsync_ShouldRemoveFighterFromRepository()
        {
            const int ExpectedFightersCount = 0;
            var user = await this.usersRepository.All().FirstAsync();
            var fighter = await this.fightersRepository.All().FirstAsync();

            await this.usersService.DeleteFighterAsync(fighter, user);
            var actualFightersCount = await this.fightersRepository.All().CountAsync();

            Assert.Equal(ExpectedFightersCount, actualFightersCount);
        }

        [Fact]
        public async Task PayForNewFighterAsync_ShouldDecreaseUserCoins()
        {
            const int ExpectedCoins = 0;
            var user = await this.usersRepository.All().FirstAsync();

            await this.usersService.PayForNewFighterAsync(user);

            Assert.Equal(ExpectedCoins, user.Coins);
        }

        [Fact]
        public async Task PayForNewTeamMemberAsync_ShouldDecreaseUserCoins()
        {
            const int ExpectedCoins = 9999;
            const int TeamMemberPrice = 1;
            var user = await this.usersRepository.All().FirstAsync();

            await this.usersService.PayForNewTeamMemberAsync(user, TeamMemberPrice);

            Assert.Equal(ExpectedCoins, user.Coins);
        }
    }
}
