namespace PugnaFighting.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using PugnaFighting.Data;
    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Data.Repositories;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Data.Cutmen;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.ViewModels.Cutmen;

    using Xunit;

    public class CutmenServiceTests
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;
        private readonly IDeletableEntityRepository<Cutman> cutmenRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Skill> skillsRepository;
        private readonly ISkillsService skillsService;
        private readonly ICutmenService cutmenService;

        public CutmenServiceTests()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            AutoMapperConfig.RegisterMappings(typeof(DetailsCutmanViewModel).Assembly);

            this.applicationDbContext = new ApplicationDbContext(options.Options);
            this.skillsRepository = new EfDeletableEntityRepository<Skill>(this.applicationDbContext);
            this.fightersRepository = new EfDeletableEntityRepository<Fighter>(this.applicationDbContext);
            this.cutmenRepository = new EfDeletableEntityRepository<Cutman>(this.applicationDbContext);
            this.skillsService = new SkillsService(this.skillsRepository, this.usersRepository);
            this.cutmenService = new CutmenService(this.cutmenRepository, this.fightersRepository, this.skillsService);

            foreach (var fighter in this.GetTestFighters())
            {
                this.fightersRepository.AddAsync(fighter);
                this.fightersRepository.SaveChangesAsync();
            }

            foreach (var cutman in this.GetTestCutmen())
            {
                this.cutmenRepository.AddAsync(cutman);
                this.cutmenRepository.SaveChangesAsync();
            }

            foreach (var skill in this.GetTestSkillPoints())
            {
                this.skillsRepository.AddAsync(skill);
                this.skillsRepository.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task AppointCutmanToFighterAsync_ShouldSetCutmanIdToFighter()
        {
            var cutman = this.GetTestCutmen().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 1);

            await this.cutmenService.AppointCutmanToFighterAsync(fighter, cutman.Id);

            Assert.Equal(fighter.CutmanId, cutman.Id);
        }

        [Fact]
        public async Task AppointCutmanToFighterAsync_ShouldIncreaseFighterHealth()
        {
            var cutman = this.GetTestCutmen().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 1);

            await this.cutmenService.AppointCutmanToFighterAsync(fighter, cutman.Id);

            Assert.Equal(67, fighter.Skill.Health);
        }

        [Fact]
        public async Task AppointCutmanToFighterAsync_HealthPointShouldntBeOverOneHundred()
        {
            const int MaxSkillPoint = 100;

            var cutman = this.GetTestCutmen().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 2);

            await this.cutmenService.AppointCutmanToFighterAsync(fighter, cutman.Id);

            Assert.Equal(MaxSkillPoint, fighter.Skill.Health);
        }

        [Fact]
        public async Task FireCutmanAsync_ShouldRemoveCutmanIdFromFighter()
        {
            var cutman = this.GetTestCutmen().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 2);
            fighter.CutmanId = cutman.Id;

            await this.cutmenService.FireCutmanAsync(fighter);

            Assert.Null(fighter.CoachId);
        }

        [Fact]
        public async Task FireCutmanAsync_ShouldDecreaseHealthPoints()
        {
            var cutman = this.GetTestCutmen().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 1);

            await this.cutmenService.AppointCutmanToFighterAsync(fighter, cutman.Id);

            Assert.Equal(67, fighter.Skill.Health);

            await this.cutmenService.FireCutmanAsync(fighter);

            Assert.Equal(65, fighter.Skill.Health);
        }

        [Fact]
        public void GetById_ShouldReturnTheSameType()
        {
            var cutman = this.cutmenService.GetById<DetailsCutmanViewModel>(1);

            Assert.IsType<DetailsCutmanViewModel>(cutman);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectObject()
        {
            const string CutmanFirstName = "Hristo";
            const string CutmanLastName = "Delev";

            var cutman = this.cutmenService.GetById<DetailsCutmanViewModel>(1);

            Assert.Equal(CutmanFirstName, cutman.FirstName);
            Assert.Equal(CutmanLastName, cutman.LastName);
        }

        [Fact]
        public void GetAll_ShouldReturnCorrectObjectCount()
        {
            const int CutmenCount = 2;

            var cutmen = this.cutmenService.GetAll<DetailsCutmanViewModel>();

            Assert.Equal(CutmenCount, cutmen.Count());
        }

        public List<Fighter> GetTestFighters()
        {
            return new List<Fighter>()
            {
                new Fighter
                {
                    Id = 1,
                    SkillId = 1,
                    BiographyId = 1,
                    OrganizationId = 1,
                    CategoryId = 3,
                    UserId = "51926c23-8a91-4e7e-94be-a97dd84bad1d",
                    RecordId = 1,
                    MoneyPerFight = 200,
                },
                new Fighter
                {
                    Id = 2,
                    SkillId = 2,
                    BiographyId = 2,
                    OrganizationId = 2,
                    CategoryId = 3,
                    UserId = "51926c23-8a91-4e7e-94be-a97dd84bad1d",
                    RecordId = 1,
                    MoneyPerFight = 200,
                },
            };
        }

        public List<Cutman> GetTestCutmen()
        {
            return new List<Cutman>()
            {
                new Cutman
                  {
                    Id = 1,
                    FirstName = "Hristo",
                    LastName = "Delev",
                    Age = 20,
                    BornCountry = "Bulgaria",
                    Price = 10,
                    HealthBonus = 2,
                    IsCustom = true,
                  },
                new Cutman
                  {
                    Id = 2,
                    FirstName = "Kiro",
                    LastName = "Roki",
                    Age = 25,
                    BornCountry = "Bulgaria",
                    Price = 11,
                    HealthBonus = 1,
                    IsCustom = true,
                  },
            };
        }

        public List<Skill> GetTestSkillPoints()
        {
            return new List<Skill>()
            {
                new Skill()
            {
                Id = 1,
                Striking = 65,
                Grappling = 65,
                Wrestling = 65,
                Stamina = 65,
                Health = 65,
                Strenght = 65,
            },
                new Skill()
            {
                Id = 2,
                Striking = 100,
                Grappling = 100,
                Wrestling = 100,
                Stamina = 100,
                Health = 100,
                Strenght = 100,
            },
            };
        }
    }
}
