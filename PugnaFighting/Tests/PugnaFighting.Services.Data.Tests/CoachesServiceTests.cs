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
    using PugnaFighting.Services.Data.Coaches;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.ViewModels.Coaches;

    using Xunit;

    public class CoachesServiceTests
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;
        private readonly IDeletableEntityRepository<Coach> coachesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Skill> skillsRepository;
        private readonly ISkillsService skillsService;
        private readonly ICoachesService coachesService;

        public CoachesServiceTests()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            AutoMapperConfig.RegisterMappings(typeof(DetailsCoachViewModel).Assembly);

            this.applicationDbContext = new ApplicationDbContext(options.Options);
            this.skillsRepository = new EfDeletableEntityRepository<Skill>(this.applicationDbContext);
            this.fightersRepository = new EfDeletableEntityRepository<Fighter>(this.applicationDbContext);
            this.coachesRepository = new EfDeletableEntityRepository<Coach>(this.applicationDbContext);
            this.skillsService = new SkillsService(this.skillsRepository, this.usersRepository);
            this.coachesService = new CoachesService(this.coachesRepository, this.fightersRepository, this.skillsService);

            foreach (var fighter in this.GetTestFighters())
            {
                this.fightersRepository.AddAsync(fighter);
                this.fightersRepository.SaveChangesAsync();
            }

            foreach (var coach in this.GetTestCoaches())
            {
                this.coachesRepository.AddAsync(coach);
                this.coachesRepository.SaveChangesAsync();
            }

            foreach (var skill in this.GetTestSkillPoints())
            {
                this.skillsRepository.AddAsync(skill);
                this.skillsRepository.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task AppointCoachToFighterAsync_ShouldSetCoachIdToFighter()
        {
            var coach = this.GetTestCoaches().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 1);

            await this.coachesService.AppointCoachToFighterAsync(fighter, coach.Id);

            Assert.Equal(fighter.CoachId, coach.Id);
        }

        [Fact]
        public async Task AppointCoachToFighterAsync_ShouldIncreaseFighterSkils()
        {
            var coach = this.GetTestCoaches().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 1);

            await this.coachesService.AppointCoachToFighterAsync(fighter, coach.Id);

            Assert.Equal(67, fighter.Skill.Grappling);
            Assert.Equal(67, fighter.Skill.Striking);
            Assert.Equal(67, fighter.Skill.Wrestling);
            Assert.Equal(67, fighter.Skill.Strenght);
            Assert.Equal(67, fighter.Skill.Stamina);
        }

        [Fact]
        public async Task AppointCoachToFighterAsync_EachSkillPointShouldntBeOverOneHundred()
        {
            const int MaxSkillPoint = 100;

            var coach = this.GetTestCoaches().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 2);

            await this.coachesService.AppointCoachToFighterAsync(fighter, coach.Id);

            Assert.Equal(MaxSkillPoint, fighter.Skill.Grappling);
            Assert.Equal(MaxSkillPoint, fighter.Skill.Striking);
            Assert.Equal(MaxSkillPoint, fighter.Skill.Wrestling);
            Assert.Equal(MaxSkillPoint, fighter.Skill.Strenght);
            Assert.Equal(MaxSkillPoint, fighter.Skill.Stamina);
        }

        [Fact]
        public async Task FireCoachAsync_ShouldRemoveCoachIdFromFighter()
        {
            var coach = this.GetTestCoaches().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 2);
            fighter.CoachId = coach.Id;

            await this.coachesService.FireCoachAsync(fighter);

            Assert.Null(fighter.CoachId);
        }

        [Fact]
        public async Task FireCoachAsync_ShouldDecreaseSkillPoints()
        {
            var coach = this.GetTestCoaches().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 1);

            await this.coachesService.AppointCoachToFighterAsync(fighter, coach.Id);

            Assert.Equal(67, fighter.Skill.Grappling);
            Assert.Equal(67, fighter.Skill.Striking);
            Assert.Equal(67, fighter.Skill.Wrestling);
            Assert.Equal(67, fighter.Skill.Strenght);
            Assert.Equal(67, fighter.Skill.Stamina);

            await this.coachesService.FireCoachAsync(fighter);

            Assert.Equal(65, fighter.Skill.Grappling);
            Assert.Equal(65, fighter.Skill.Striking);
            Assert.Equal(65, fighter.Skill.Wrestling);
            Assert.Equal(65, fighter.Skill.Strenght);
            Assert.Equal(65, fighter.Skill.Stamina);
        }

        [Fact]
        public void GetById_ShouldReturnTheSameType()
        {
            var coach = this.coachesService.GetById<DetailsCoachViewModel>(1);

            Assert.IsType<DetailsCoachViewModel>(coach);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectObject()
        {
            const string CoachFirstName = "Gosho";
            const string CoachLastName = "Denichin";

            var coach = this.coachesService.GetById<DetailsCoachViewModel>(1);

            Assert.Equal(CoachFirstName, coach.FirstName);
            Assert.Equal(CoachLastName, coach.LastName);
        }

        [Fact]
        public void GetAll_ShouldReturnCorrectObjectCount()
        {
            const int CoachesCount = 2;

            var coaches = this.coachesService.GetAll<DetailsCoachViewModel>();

            Assert.Equal(CoachesCount, coaches.Count());
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

        public List<Coach> GetTestCoaches()
        {
            return new List<Coach>()
            {
                new Coach
                  {
                    Id = 1,
                    FirstName = "Gosho",
                    LastName = "Denichin",
                    Age = 20,
                    BornCountry = "Bulgaria",
                    Price = 10,
                    SkillBonus = 2,
                    IsCustom = true,
                  },
                new Coach
                  {
                    Id = 2,
                    FirstName = "Vili",
                    LastName = "Denichin",
                    Age = 25,
                    BornCountry = "Bulgaria",
                    Price = 11,
                    SkillBonus = 1,
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
