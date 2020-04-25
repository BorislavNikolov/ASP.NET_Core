namespace PugnaFighting.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using PugnaFighting.Data;
    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Data.Repositories;

    using Xunit;

    public class OrganizationsServiceTests
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDeletableEntityRepository<Organization> organizationsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;
        private readonly IOrganizationsService organizationsService;

        public OrganizationsServiceTests()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options =
               new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.applicationDbContext = new ApplicationDbContext(options.Options);
            this.organizationsRepository = new EfDeletableEntityRepository<Organization>(this.applicationDbContext);
            this.fightersRepository = new EfDeletableEntityRepository<Fighter>(this.applicationDbContext);
            this.usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.applicationDbContext);
            this.organizationsService = new OrganizationsService(this.organizationsRepository, this.usersRepository, this.fightersRepository);

            var organization = new Organization()
            {
                Id = 5,
                Name = "UFC",
                Location = "USA",
                FansCount = 100,
                InstantCash = 10000,
                MoneyPerFight = 3000,
            };

            this.organizationsRepository.AddAsync(organization);
            this.organizationsRepository.SaveChangesAsync();

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
            };

            this.usersRepository.AddAsync(user);
            this.usersRepository.SaveChangesAsync();
        }

        [Fact]
        public async Task SetOrganizationAsync_ShouldIncreaseTheUserCoins()
        {
            const int ExpectedUserCoins = 20000;
            var fighter = this.fightersRepository.All().Where(x => x.Id == 2).FirstOrDefault();
            var user = this.usersRepository.All().Where(x => x.Id == "51926c23-8a91-4e7e-94be-a97dd84bad1d").FirstOrDefault();

            await this.organizationsService.SetOrganizationAsync(fighter, 5, user);

            Assert.Equal(ExpectedUserCoins, user.Coins);
        }

        [Fact]
        public async Task SetOrganizationAsync_ShouldSetOrganizationIdToFighter()
        {
            const int ExpectedOrganizationId = 5;
            var fighter = this.fightersRepository.All().Where(x => x.Id == 2).FirstOrDefault();
            var user = this.usersRepository.All().Where(x => x.Id == "51926c23-8a91-4e7e-94be-a97dd84bad1d").FirstOrDefault();

            await this.organizationsService.SetOrganizationAsync(fighter, 5, user);

            Assert.Equal(ExpectedOrganizationId, fighter.OrganizationId);
        }

        [Fact]
        public async Task SetOrganizationAsync_ShouldIncreaseFighterFansCountAndMoneyPerFight()
        {
            const int ExpectedFansCount = 400;
            const int ExpectedMoneyPerFight = 3200;
            var fighter = this.fightersRepository.All().Where(x => x.Id == 2).FirstOrDefault();
            var user = this.usersRepository.All().Where(x => x.Id == "51926c23-8a91-4e7e-94be-a97dd84bad1d").FirstOrDefault();

            await this.organizationsService.SetOrganizationAsync(fighter, 5, user);

            Assert.Equal(ExpectedFansCount, fighter.FansCount);
            Assert.Equal(ExpectedMoneyPerFight, fighter.MoneyPerFight);
        }
    }
}
