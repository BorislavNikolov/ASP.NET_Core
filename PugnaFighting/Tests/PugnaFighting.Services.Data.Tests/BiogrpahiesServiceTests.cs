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
    using Xunit;

    public class BiogrpahiesServiceTests
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDeletableEntityRepository<Biography> biographiesRepository;
        private readonly IBiographiesService biographiesService;
        private readonly ICloudinaryService cloudinaryService;

        public BiogrpahiesServiceTests()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options =
               new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());

            Account account = new Account("Cloudinary", "Test", "Account");
            Cloudinary cloudinary = new Cloudinary(account);

            this.applicationDbContext = new ApplicationDbContext(options.Options);
            this.biographiesRepository = new EfDeletableEntityRepository<Biography>(this.applicationDbContext);
            this.cloudinaryService = new CloudinaryService(cloudinary);
            this.biographiesService = new BiogrpahiesService(this.biographiesRepository, this.cloudinaryService);

            foreach (var biography in this.GetTestBiographies())
            {
                this.biographiesRepository.AddAsync(biography);
                this.biographiesRepository.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnNewBiographyId()
        {
            const int ExpectedBiographyId = 3;

            var biographyId = await this.biographiesService.CreateAsync("John", "Captain", "Terry", "England", 2, null);

            Assert.Equal(ExpectedBiographyId, biographyId);
        }

        [Fact]
        public async Task CreateAsync_ShouldSaveBiographyIntoRepository()
        {
            const int ExpectedRepostitoryCount = 3;

            await this.biographiesService.CreateAsync("John", "Captain", "Terry", "England", 2, null);
            var biographyRepositoryCount = await this.biographiesRepository.All().CountAsync();

            Assert.Equal(ExpectedRepostitoryCount, biographyRepositoryCount);
        }

        [Fact]
        public async Task CreateAsync_ShouldSaveTheDataCorrectly()
        {
            const string ExpectedFirstName = "John";
            const string ExpectedNickname = "Captain";
            const string ExpectedLastName = "Terry";
            const string ExpectedBornCountry = "England";
            const int ExpectedFirstAge = 2;
            const string ExpectedPictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1586529578/FightersPics/Unknown_olk6sa.jpg";

            var biographyId = await this.biographiesService.CreateAsync("John", "Captain", "Terry", "England", 2, null);
            var biography = this.biographiesRepository.All().Where(x => x.Id == biographyId).FirstOrDefault();

            Assert.Equal(ExpectedFirstName, biography.FirstName);
            Assert.Equal(ExpectedNickname, biography.Nickname);
            Assert.Equal(ExpectedLastName, biography.LastName);
            Assert.Equal(ExpectedBornCountry, biography.BornCountry);
            Assert.Equal(ExpectedFirstAge, biography.Age);
            Assert.Equal(ExpectedPictureUrl, biography.PictureUrl);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectBiography()
        {
            const string ExpectedFirstName = "Kiro";
            const string ExpectedNickname = "Killer-a";
            const string ExpectedLastName = "Kirov";
            const string ExpectedBornCountry = "Bulgaria";
            const int ExpectedFirstAge = 25;
            const string ExpectedPictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1586529578/FightersPics/Unknown_olk6sa.jpg";

            var biography = this.biographiesService.GetById(2);

            Assert.Equal(ExpectedFirstName, biography.FirstName);
            Assert.Equal(ExpectedNickname, biography.Nickname);
            Assert.Equal(ExpectedLastName, biography.LastName);
            Assert.Equal(ExpectedBornCountry, biography.BornCountry);
            Assert.Equal(ExpectedFirstAge, biography.Age);
            Assert.Equal(ExpectedPictureUrl, biography.PictureUrl);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDecreaseRepositoryCount()
        {
            const int ExpectedCountAfterDelete = 1;
            var biographiesCount = await this.biographiesRepository.All().CountAsync();

            Assert.Equal(2, biographiesCount);

            await this.biographiesService.DeleteAsync(2);

            biographiesCount = await this.biographiesRepository.All().CountAsync();

            Assert.Equal(ExpectedCountAfterDelete, biographiesCount);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSetIsDeleteToTrue()
        {
            await this.biographiesService.DeleteAsync(2);

            var biography = this.biographiesService.GetById(2);

            Assert.Null(biography);
        }

        public List<Biography> GetTestBiographies()
        {
            return new List<Biography>()
            {
                new Biography
                  {
                    Id = 1,
                    FirstName = "Petar",
                    Nickname = "Predator",
                    LastName = "Hristov",
                    Age = 21,
                    BornCountry = "Bulgaria",
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1586529578/FightersPics/Unknown_olk6sa.jpg",
                  },
                new Biography
                  {
                    Id = 2,
                    FirstName = "Kiro",
                    Nickname = "Killer-a",
                    LastName = "Kirov",
                    Age = 25,
                    BornCountry = "Bulgaria",
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1586529578/FightersPics/Unknown_olk6sa.jpg",
                  },
            };
        }
    }
}
