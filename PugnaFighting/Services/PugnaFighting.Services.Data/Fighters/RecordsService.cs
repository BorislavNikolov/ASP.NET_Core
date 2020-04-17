namespace PugnaFighting.Services.Data.Fighters
{
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data.Contracts;

    public class RecordsService : IRecordsService
    {
        private readonly IDeletableEntityRepository<Record> recordsRepository;

        public RecordsService(IDeletableEntityRepository<Record> recordsRepository)
        {
            this.recordsRepository = recordsRepository;
        }

        public async Task<int> CreateAsync()
        {
            var record = new Record
            {
            };

            await this.recordsRepository.AddAsync(record);
            await this.recordsRepository.SaveChangesAsync();

            return record.Id;
        }
    }
}
