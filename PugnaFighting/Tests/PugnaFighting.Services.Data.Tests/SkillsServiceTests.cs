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
    using PugnaFighting.Web.ViewModels.Skills;

    using Xunit;

    public class SkillsServiceTests
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDeletableEntityRepository<Skill> skillsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly ISkillsService skillsService;

        public SkillsServiceTests()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.applicationDbContext = new ApplicationDbContext(options.Options);
            this.skillsRepository = new EfDeletableEntityRepository<Skill>(this.applicationDbContext);
            this.usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.applicationDbContext);
            this.skillsService = new SkillsService(this.skillsRepository, this.usersRepository);

            var skill = new Skill()
            {
                Id = 1,
                Striking = 65,
                Grappling = 65,
                Wrestling = 65,
                Stamina = 65,
                Health = 65,
                Strenght = 65,
            };

            this.skillsRepository.AddAsync(skill);
            this.skillsRepository.SaveChangesAsync();

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
        public async Task CreateAsync_ShouldAddNewSkillIntoRepository()
        {
            const int ExpectedSkillsCount = 2;

            await this.skillsService.CreateAsync();
            var actualSkillsCount = await this.skillsRepository.All().CountAsync();

            Assert.Equal(ExpectedSkillsCount, actualSkillsCount);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveSkillFromRepository()
        {
            const int ExpectedSkillsCount = 0;

            await this.skillsService.DeleteAsync(1);
            var actualSkillsCount = await this.skillsRepository.All().CountAsync();

            Assert.Equal(ExpectedSkillsCount, actualSkillsCount);
        }

        [Fact]
        public async Task UpdateSkillPointsAsync_ShouldSetNewSkillPointsCorrectly()
        {
            var trainViewModel = new TrainViewModel()
            {
                Striking = 70,
                Grappling = 70,
                Wrestling = 70,
                Stamina = 70,
                Health = 70,
                Strenght = 70,
            };

            await this.skillsService.UpdateSkillPointsAsync(trainViewModel, 1);
            var updatedSkill = await this.skillsRepository.All().FirstAsync();

            Assert.Equal(70, updatedSkill.Striking);
            Assert.Equal(70, updatedSkill.Grappling);
            Assert.Equal(70, updatedSkill.Wrestling);
            Assert.Equal(70, updatedSkill.Strenght);
            Assert.Equal(70, updatedSkill.Stamina);
            Assert.Equal(70, updatedSkill.Health);
        }

        [Fact]
        public void GetSkillPointsOverall_ShouldReturnCorrectOverallPoints()
        {
            const int ExpectedSkillPointsOverall = 390;
            var skill = this.skillsRepository.All().Where(x => x.Id == 1).FirstOrDefault();

            var actualSkillPointsOverall = this.skillsService.GetSkillPointsOverall(skill);

            Assert.Equal(ExpectedSkillPointsOverall, actualSkillPointsOverall);
        }

        [Fact]
        public void ChechForEnoughCoinsToTrain_ShouldReturnTrue_WhenCoinsAreEnough()
        {
            var trainViewModel = new TrainViewModel()
            {
                Striking = 66,
                Grappling = 66,
                Wrestling = 66,
                Stamina = 66,
                Health = 66,
                Strenght = 66,
            };

            var user = this.usersRepository.All().Where(x => x.Id == "51926c23-8a91-4e7e-94be-a97dd84bad1d").FirstOrDefault();
            var skill = this.skillsRepository.All().Where(x => x.Id == 1).FirstOrDefault();

            var result = this.skillsService.ChechForEnoughCoinsToTrain(user, skill, trainViewModel);

            Assert.True(result);
        }

        [Fact]
        public void ChechForEnoughCoinsToTrain_ShouldReturnFalse_WhenCoinsAreNotEnough()
        {
            var trainViewModel = new TrainViewModel()
            {
                Striking = 76,
                Grappling = 76,
                Wrestling = 76,
                Stamina = 76,
                Health = 76,
                Strenght = 76,
            };

            var user = this.usersRepository.All().Where(x => x.Id == "51926c23-8a91-4e7e-94be-a97dd84bad1d").FirstOrDefault();
            var skill = this.skillsRepository.All().Where(x => x.Id == 1).FirstOrDefault();

            var result = this.skillsService.ChechForEnoughCoinsToTrain(user, skill, trainViewModel);

            Assert.False(result);
        }
    }
}
