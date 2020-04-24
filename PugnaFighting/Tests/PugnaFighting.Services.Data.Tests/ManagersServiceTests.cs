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
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Data.Managers;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.ViewModels.Managers;

    using Xunit;

    public class ManagersServiceTests
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;
        private readonly IDeletableEntityRepository<Manager> managersRepository;
        private readonly IManagersService managersService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly CreateManagerViewModel createManagerViewModel;

        public ManagersServiceTests()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            AutoMapperConfig.RegisterMappings(typeof(DetailsManagerViewModel).Assembly);

            Account account = new Account("Cloudinary", "Test", "Account");
            Cloudinary cloudinary = new Cloudinary(account);

            this.applicationDbContext = new ApplicationDbContext(options.Options);
            this.fightersRepository = new EfDeletableEntityRepository<Fighter>(this.applicationDbContext);
            this.managersRepository = new EfDeletableEntityRepository<Manager>(this.applicationDbContext);
            this.cloudinaryService = new CloudinaryService(cloudinary);
            this.managersService = new ManagersService(this.managersRepository, this.fightersRepository, this.cloudinaryService);

            this.createManagerViewModel = new CreateManagerViewModel()
            {
                FirstName = "Borislav",
                LastName = "Nikolov",
                BornCountry = "USA",
                Age = 33,
                MoneyPerFight = 700,
                FansCount = 200,
            };

            foreach (var fighter in this.GetTestFighters())
            {
                this.fightersRepository.AddAsync(fighter);
                this.fightersRepository.SaveChangesAsync();
            }

            foreach (var manager in this.GetTestManagers())
            {
                this.managersRepository.AddAsync(manager);
                this.managersRepository.SaveChangesAsync();
            }
        }

        [Fact]
        public void GetById_ShouldReturnTheSameType()
        {
            var manager = this.managersService.GetById<DetailsManagerViewModel>(1);

            Assert.IsType<DetailsManagerViewModel>(manager);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectObject()
        {
            const string ManagerFirstName = "Peter";
            const string ManagerLastName = "Jacobs";

            var manager = this.managersService.GetById<DetailsManagerViewModel>(1);

            Assert.Equal(ManagerFirstName, manager.FirstName);
            Assert.Equal(ManagerLastName, manager.LastName);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCorrectId()
        {
            const int CorrectId = 3;

            var managerId = await this.managersService.CreateAsync(this.createManagerViewModel);

            Assert.Equal(CorrectId, managerId);
        }

        [Fact]
        public async Task CreateAsync_ShouldSetNewManagerIntoRepository()
        {
            const int ExpectedManagersCount = 3;

            await this.managersService.CreateAsync(this.createManagerViewModel);
            var actualManagersCount = await this.managersRepository.All().CountAsync();

            Assert.Equal(ExpectedManagersCount, actualManagersCount);
        }

        [Fact]
        public async Task CreateAsync_ShouldSetDataCorrectly()
        {
            const string ExpectedFirstName = "Borislav";
            const string ExpectedLastName = "Nikolov";
            const string ExpectedBornCountry = "USA";
            const int ExpectedAge = 33;
            const int ExpectedPrice = 20000;
            const int ExpectedMoneyPerFight = 700;
            const int ExpectedFansCount = 200;

            var managerId = await this.managersService.CreateAsync(this.createManagerViewModel);
            var manager = this.managersRepository.All().Where(x => x.Id == managerId).FirstOrDefault();

            Assert.Equal(ExpectedFirstName, manager.FirstName);
            Assert.Equal(ExpectedLastName, manager.LastName);
            Assert.Equal(ExpectedBornCountry, manager.BornCountry);
            Assert.Equal(ExpectedAge, manager.Age);
            Assert.Equal(ExpectedPrice, manager.Price);
            Assert.Equal(ExpectedMoneyPerFight, manager.MoneyPerFight);
            Assert.Equal(ExpectedFansCount, manager.FansCount);
        }

        [Fact]
        public async Task CreateAsync_ShouldSetUnknownPicture_WhenPictureIsNull()
        {
            const string ExpectedPictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1586529578/FightersPics/Unknown_olk6sa.jpg";

            var managerId = await this.managersService.CreateAsync(this.createManagerViewModel);
            var manager = this.managersRepository.All().Where(x => x.Id == managerId).FirstOrDefault();

            Assert.Equal(ExpectedPictureUrl, manager.PictureUrl);
        }

        [Fact]
        public async Task AppointManagerToFighterAsync_ShouldIncreaseFighterFansCountAndMoneyPerFight()
        {
            const int ExpectedFansCount = 600;
            const int ExpectedMoneyPerFight = 1900;
            var manager = this.GetTestManagers().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 1);

            await this.managersService.AppointManagerToFighterAsync(fighter, manager.Id);

            Assert.Equal(ExpectedFansCount, fighter.FansCount);
            Assert.Equal(ExpectedMoneyPerFight, fighter.MoneyPerFight);
        }

        [Fact]
        public async Task AppointManagerToFighterAsync_ShouldSetManagerIdToFighter()
        {
            const int ExpectedManagerId = 1;
            var manager = this.GetTestManagers().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 1);

            await this.managersService.AppointManagerToFighterAsync(fighter, manager.Id);

            Assert.Equal(ExpectedManagerId, fighter.ManagerId);
        }

        [Fact]
        public async Task FireManagerAsync_ShouldDecreaseFighterFansCountAndMoneyPerFight()
        {
            var manager = this.GetTestManagers().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 1);

            await this.managersService.AppointManagerToFighterAsync(fighter, manager.Id);

            Assert.Equal(600, fighter.FansCount);
            Assert.Equal(1900, fighter.MoneyPerFight);

            await this.managersService.FireManagerAsync(fighter);

            Assert.Equal(100, fighter.FansCount);
            Assert.Equal(200, fighter.MoneyPerFight);
        }

        [Fact]
        public async Task FireManagerAsync_ShouldntDecreaseFighterFansCountBelowZero()
        {
            var manager = this.GetTestManagers().FirstOrDefault(x => x.Id == 1);
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 1);

            await this.managersService.AppointManagerToFighterAsync(fighter, manager.Id);
            fighter.FansCount = 1;

            Assert.Equal(1, fighter.FansCount);
            Assert.Equal(1900, fighter.MoneyPerFight);

            await this.managersService.FireManagerAsync(fighter);

            Assert.Equal(0, fighter.FansCount);
            Assert.Equal(200, fighter.MoneyPerFight);
        }

        [Fact]
        public async Task FireManagerAsync_ShouldRemoveManagerIdFromFighter()
        {
            var fighter = this.GetTestFighters().FirstOrDefault(x => x.Id == 2);
            fighter.ManagerId = 1;

            await this.managersService.FireManagerAsync(fighter);

            Assert.Null(fighter.ManagerId);
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
                    UserId = "51926c23-8a91-4e7e-94be-a97dd84bad1d",
                    RecordId = 1,
                    MoneyPerFight = 200,
                    FansCount = 100,
                },
            };
        }

        public List<Manager> GetTestManagers()
        {
            return new List<Manager>()
            {
                new Manager
                  {
                    Id = 1,
                    FirstName = "Peter",
                    LastName = "Jacobs",
                    Age = 20,
                    BornCountry = "England",
                    Price = 10,
                    MoneyPerFight = 1700,
                    FansCount = 500,
                    IsCustom = true,
                  },
                new Manager
                  {
                    Id = 2,
                    FirstName = "Daniel",
                    LastName = "Kirov",
                    Age = 25,
                    BornCountry = "Bulgaria",
                    Price = 11,
                    MoneyPerFight = 700,
                    FansCount = 600,
                    IsCustom = true,
                  },
            };
        }
    }
}
