namespace PugnaFighting.Services.Data.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using PugnaFighting.Data;
    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Data.Repositories;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Data.Fighters;

    using Xunit;

    public class RecordsServiceTests
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDeletableEntityRepository<PugnaFighting.Data.Models.Record> recordsRepository;
        private readonly IDeletableEntityRepository<Fight> fightsRepository;
        private readonly IRecordsService recordsService;

        public RecordsServiceTests()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options =
               new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.applicationDbContext = new ApplicationDbContext(options.Options);
            this.recordsRepository = new EfDeletableEntityRepository<PugnaFighting.Data.Models.Record>(this.applicationDbContext);
            this.fightsRepository = new EfDeletableEntityRepository<Fight>(this.applicationDbContext);
            this.recordsService = new RecordsService(this.recordsRepository, this.fightsRepository);
        }

        [Fact]
        public async void CreateAsync_ShouldSaveNewRecordIntoRepository()
        {
            const int ExpectedRecordsCount = 1;

            await this.recordsService.CreateAsync();
            var recordsCount = await this.recordsRepository.All().CountAsync();

            Assert.Equal(ExpectedRecordsCount, recordsCount);
        }
    }
}
