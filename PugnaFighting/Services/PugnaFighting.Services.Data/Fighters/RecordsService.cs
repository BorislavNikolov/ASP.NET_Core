namespace PugnaFighting.Services.Data.Fighters
{
    using System.Linq;
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
                Wins = 0,
                Draws = 0,
                Losses = 0,
            };

            await this.recordsRepository.AddAsync(record);
            await this.recordsRepository.SaveChangesAsync();

            return record.Id;
        }

        public Record GetById(int id)
        {
            var record = this.recordsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            return record;
        }
    }
}
