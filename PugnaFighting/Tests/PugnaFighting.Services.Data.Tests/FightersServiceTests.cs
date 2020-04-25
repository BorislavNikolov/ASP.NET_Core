namespace PugnaFighting.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;

    using Microsoft.EntityFrameworkCore;

    using PugnaFighting.Data;
    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Data.Repositories;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.ViewModels.Fighters;

    using Xunit;

    public class FightersServiceTests
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;
        private readonly IDeletableEntityRepository<Biography> biographiesRepository;
        private readonly IDeletableEntityRepository<Fight> fightsRepository;
        private readonly IDeletableEntityRepository<Skill> skillsRepository;
        private readonly IDeletableEntityRepository<PugnaFighting.Data.Models.Record> recordsRepository;
        private readonly IDeletableEntityRepository<Organization> organizationsRepository;
        private readonly IBiographiesService biographiesService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ISkillsService skillsService;
        private readonly IFightersService fightersService;

        public FightersServiceTests()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            Account account = new Account("Cloudinary", "Test", "Account");
            Cloudinary cloudinary = new Cloudinary(account);

            AutoMapperConfig.RegisterMappings(typeof(FightersDropDownViewModel).Assembly);

            this.applicationDbContext = new ApplicationDbContext(options.Options);
            this.fightersRepository = new EfDeletableEntityRepository<Fighter>(this.applicationDbContext);
            this.usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.applicationDbContext);
            this.fightsRepository = new EfDeletableEntityRepository<Fight>(this.applicationDbContext);
            this.recordsRepository = new EfDeletableEntityRepository<PugnaFighting.Data.Models.Record>(this.applicationDbContext);
            this.biographiesRepository = new EfDeletableEntityRepository<Biography>(this.applicationDbContext);
            this.skillsRepository = new EfDeletableEntityRepository<Skill>(this.applicationDbContext);
            this.organizationsRepository = new EfDeletableEntityRepository<Organization>(this.applicationDbContext);
            this.cloudinaryService = new CloudinaryService(cloudinary);
            this.biographiesService = new BiogrpahiesService(this.biographiesRepository, this.cloudinaryService);
            this.skillsService = new SkillsService(this.skillsRepository, this.usersRepository);
            this.fightersService = new FightersService(
                this.fightersRepository,
                this.recordsRepository,
                this.fightsRepository,
                this.usersRepository,
                this.biographiesService,
                this.skillsService);

            var user = new ApplicationUser()
            {
                Id = "51926c23-8a91-4e7e-94be-a97dd84bad1d",
                Coins = 10000,
                UserName = "TheBestFighter",
                FightersCount = 3,
            };

            this.usersRepository.AddAsync(user);
            this.usersRepository.SaveChangesAsync();

            foreach (var fighter in this.GetTestFighters())
            {
                this.fightersRepository.AddAsync(fighter);
                this.fightersRepository.SaveChangesAsync();
            }

            foreach (var skill in this.GetTestSkills())
            {
                this.skillsRepository.AddAsync(skill);
                this.skillsRepository.SaveChangesAsync();
            }

            foreach (var record in this.GetTestRecords())
            {
                this.recordsRepository.AddAsync(record);
                this.recordsRepository.SaveChangesAsync();
            }

            foreach (var biography in this.GetTestBiographies())
            {
                this.biographiesRepository.AddAsync(biography);
                this.biographiesRepository.SaveChangesAsync();
            }

            foreach (var organization in this.GetTestOrganizations())
            {
                this.organizationsRepository.AddAsync(organization);
                this.organizationsRepository.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task CreateAsync_ShouldSetNewFighterIntoRepository()
        {
            const int ExpectedFightersCount = 4;
            var user = await this.usersRepository.All().FirstAsync();

            await this.fightersService.CreateAsync(1, 1, 1, 4, user);
            var actualFightersCount = await this.fightersRepository.All().CountAsync();

            Assert.Equal(ExpectedFightersCount, actualFightersCount);
        }

        [Fact]
        public async Task CreateAsync_ShouldSetNewFighterIntoUserFighters()
        {
            const int ExpectedFightersCount = 4;
            var user = await this.usersRepository.All().FirstAsync();

            await this.fightersService.CreateAsync(1, 1, 1, 4, user);
            var actualFightersCount = user.FightersCount;

            Assert.Equal(ExpectedFightersCount, actualFightersCount);
        }

        [Fact]
        public void GetAllFightersWithoutManagers_ShouldReturnCorrectFightersCount()
        {
            const int ExpectedFightersCount = 1;

            var result = this.fightersService.GetAllFightersWithoutManagers<FightersDropDownViewModel>("51926c23-8a91-4e7e-94be-a97dd84bad1d");
            var actualFightersCount = result.ToList().Count;

            Assert.Equal(ExpectedFightersCount, actualFightersCount);
        }

        [Fact]
        public void GetAllFightersWithoutCoaches_ShouldReturnCorrectFightersCount()
        {
            const int ExpectedFightersCount = 1;

            var result = this.fightersService.GetAllFightersWithoutCoaches<FightersDropDownViewModel>("51926c23-8a91-4e7e-94be-a97dd84bad1d");
            var actualFightersCount = result.ToList().Count;

            Assert.Equal(ExpectedFightersCount, actualFightersCount);
        }

        [Fact]
        public void GetAllFightersWithoutCutmen_ShouldReturnCorrectFightersCount()
        {
            const int ExpectedFightersCount = 1;

            var result = this.fightersService.GetAllFightersWithoutCutmen<FightersDropDownViewModel>("51926c23-8a91-4e7e-94be-a97dd84bad1d");
            var actualFightersCount = result.ToList().Count;

            Assert.Equal(ExpectedFightersCount, actualFightersCount);
        }

        [Fact]
        public void GetBestStriker_ShouldReturnFighterWithTheMostStrikingPoints()
        {
            const int ExpectedFighterId = 1;

            var result = this.fightersService.GetBestStriker<FightersDropDownViewModel>("Animo");
            var actualFighterId = result.Id;

            Assert.Equal(ExpectedFighterId, actualFighterId);
        }

        [Fact]
        public void GetBestWrestler_ShouldReturnFighterWithTheMostWrestlingPoints()
        {
            const int ExpectedFighterId = 3;

            var result = this.fightersService.GetBestStriker<FightersDropDownViewModel>("Corporis");
            var actualFighterId = result.Id;

            Assert.Equal(ExpectedFighterId, actualFighterId);
        }

        [Fact]
        public void GetBestGrappler_ShouldReturnFighterWithTheMosGrapplingPoints()
        {
            const int ExpectedFighterId = 3;

            var result = this.fightersService.GetBestStriker<FightersDropDownViewModel>("Corporis");
            var actualFighterId = result.Id;

            Assert.Equal(ExpectedFighterId, actualFighterId);
        }

        [Fact]
        public void GetAllOpponents_ShouldntReturnFightersFromCurrentUser()
        {
            const int CurrentUserFighterId = 1;

            var result = this.fightersService.GetAllOpponents<FightersDropDownViewModel>("51926c23-8a91-4e7e-94be-a97dd84bad1d");

            foreach (var fighter in result)
            {
                Assert.NotEqual(CurrentUserFighterId, fighter.Id);
            }
        }

        [Fact]
        public async Task FightAsync_ShouldReturnObjectFromTypeFight()
        {
            var fighter = await this.fightersRepository.All().LastAsync();
            var opponent = await this.fightersRepository.All().FirstAsync();
            var user = await this.usersRepository.All().FirstAsync();

            var result = await this.fightersService.FightAsync(fighter, opponent, user);

            Assert.IsType<Fight>(result);
        }

        [Fact]
        public async Task FightAsync_ShouldIncreaseUserCoins()
        {
            var fighter = await this.fightersRepository.All().LastAsync();
            var opponent = await this.fightersRepository.All().FirstAsync();
            var user = await this.usersRepository.All().FirstAsync();

            await this.fightersService.FightAsync(fighter, opponent, user);

            Assert.True(user.Coins > 10000);
        }

        [Fact]
        public async Task FightAsync_ChangeFighterFansCount()
        {
            var fighter = await this.fightersRepository.All().LastAsync();
            var opponent = await this.fightersRepository.All().FirstAsync();
            var user = await this.usersRepository.All().FirstAsync();

            await this.fightersService.FightAsync(fighter, opponent, user);

            Assert.True(fighter.FansCount != 300);
        }

        [Fact]
        public async Task FightAsync_ShouldAddNewFightIntoRepository()
        {
            const int ExpectedFightsCount = 1;
            var fighter = await this.fightersRepository.All().LastAsync();
            var opponent = await this.fightersRepository.All().FirstAsync();
            var user = await this.usersRepository.All().FirstAsync();

            await this.fightersService.FightAsync(fighter, opponent, user);
            var actualFightsCount = await this.fightsRepository.All().CountAsync();

            Assert.Equal(ExpectedFightsCount, actualFightsCount);
        }

        [Fact]
        public async Task AddFightToRecordAsync_ShouldIncreaseWins_WhenFightResultIsWin()
        {
            const int ExpectedWins = 16;
            var fighter = await this.fightersRepository.All().LastAsync();
            var opponent = await this.fightersRepository.All().FirstAsync();
            var user = await this.usersRepository.All().FirstAsync();

            var fight = await this.fightersService.FightAsync(fighter, opponent, user);

            await this.fightersService.AddFightToRecordAsync(fight, fighter);
            var actualWins = fighter.Record.Wins;

            Assert.Equal(ExpectedWins, actualWins);
        }

        [Fact]
        public async Task AddFightToRecordAsync_ShouldIncreaseDraws_WhenFightResultIsDraw()
        {
            const int ExpectedDraws = 1;
            var fighter = await this.fightersRepository.All().LastAsync();
            var opponent = this.fightersRepository.All().Where(x => x.Id == 2).FirstOrDefault();
            var user = await this.usersRepository.All().FirstAsync();

            var fight = await this.fightersService.FightAsync(fighter, opponent, user);

            await this.fightersService.AddFightToRecordAsync(fight, fighter);
            var actualDraws = fighter.Record.Draws;

            Assert.Equal(ExpectedDraws, actualDraws);
        }

        [Fact]
        public async Task AddFightToRecordAsync_ShouldIncreaseLosses_WhenFightResultIsLose()
        {
            const int ExpectedLosses = 5;
            var fighter = await this.fightersRepository.All().FirstAsync();
            var opponent = await this.fightersRepository.All().LastAsync();
            var user = await this.usersRepository.All().FirstAsync();

            var fight = await this.fightersService.FightAsync(fighter, opponent, user);

            await this.fightersService.AddFightToRecordAsync(fight, fighter);
            var actualLosses = fighter.Record.Losses;

            Assert.Equal(ExpectedLosses, actualLosses);
        }

        public List<Organization> GetTestOrganizations()
        {
            return new List<Organization>()
            {
                new Organization
                {
                    Id = 1,
                    Name = "Animo",
                    Location = "Greece",
                    InstantCash = 1111,
                    MoneyPerFight = 200,
                    FansCount = 100,
                },
                new Organization
                {
                    Id = 2,
                    Name = "Corporis",
                    Location = "Poland",
                    InstantCash = 11211,
                    MoneyPerFight = 200,
                    FansCount = 100,
                },
            };
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
                    FansCount = 100,
                },
                new Fighter
                {
                    Id = 2,
                    SkillId = 2,
                    BiographyId = 2,
                    OrganizationId = 2,
                    CategoryId = 3,
                    UserId = "51926c23-8a91-4e7e-94ae-a97dd84bad1d",
                    RecordId = 1,
                    MoneyPerFight = 200,
                    FansCount = 100,
                },
                new Fighter
                {
                    Id = 3,
                    SkillId = 3,
                    BiographyId = 3,
                    OrganizationId = 2,
                    CategoryId = 1,
                    UserId = "51926c23-8a11-4e7e-94be-a97dd84bad1d",
                    RecordId = 2,
                    MoneyPerFight = 200,
                    FansCount = 300,
                },
            };
        }

        public List<Skill> GetTestSkills()
        {
            return new List<Skill>()
            {
                new Skill
                {
                    Id = 1,
                    Striking = 65,
                    Grappling = 65,
                    Wrestling = 65,
                    Stamina = 65,
                    Health = 65,
                    Strenght = 65,
                },
                new Skill
                {
                    Id = 2,
                    Striking = 95,
                    Grappling = 95,
                    Wrestling = 95,
                    Stamina = 95,
                    Health = 95,
                    Strenght = 95,
                },
                new Skill
                {
                    Id = 3,
                    Striking = 95,
                    Grappling = 95,
                    Wrestling = 95,
                    Stamina = 95,
                    Health = 95,
                    Strenght = 95,
                },
            };
        }

        public List<PugnaFighting.Data.Models.Record> GetTestRecords()
        {
            return new List<PugnaFighting.Data.Models.Record>()
            {
                new PugnaFighting.Data.Models.Record
                {
                    Id = 1,
                    Wins = 2,
                    Draws = 1,
                    Losses = 4,
                },
                new PugnaFighting.Data.Models.Record
                {
                    Id = 2,
                    Wins = 15,
                    Draws = 0,
                    Losses = 0,
                },
            };
        }

        public List<Biography> GetTestBiographies()
        {
            return new List<Biography>()
            {
                new Biography
                {
                    Id = 1,
                    FirstName = "Mark",
                    Nickname = "The Monster",
                    LastName = "Henry",
                    Age = 21,
                    BornCountry = "Canada",
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1586529578/FightersPics/Unknown_olk6sa.jpg",
                },
                new Biography
                {
                    Id = 2,
                    FirstName = "Petar",
                    Nickname = "Predator",
                    LastName = "Hristov",
                    Age = 25,
                    BornCountry = "Bulgaria",
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1586529578/FightersPics/Unknown_olk6sa.jpg",
                },
                new Biography
                {
                    Id = 3,
                    FirstName = "Sebastian",
                    Nickname = "The Flash",
                    LastName = "Iliev",
                    Age = 31,
                    BornCountry = "Turkey",
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1586529578/FightersPics/Unknown_olk6sa.jpg",
                },
            };
        }
    }
}
