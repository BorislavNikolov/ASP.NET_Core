namespace PugnaFighting.Services.Data.Fighters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Mapping;

    public class RecordsService : IRecordsService
    {
        private readonly IDeletableEntityRepository<Record> recordsRepository;
        private readonly IDeletableEntityRepository<Fight> fightsRepository;

        public RecordsService(IDeletableEntityRepository<Record> recordsRepository, IDeletableEntityRepository<Fight> fightsRepository)
        {
            this.recordsRepository = recordsRepository;
            this.fightsRepository = fightsRepository;
        }

        public async Task<int> CreateAsync()
        {
            var record = new Record
            {
                Wins = 0,
                Draws = 0,
                Losses = 0,
                Fights = new List<Fight>(),
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

        public T GetFight<T>(int fightId)
        {
            var fight = this.fightsRepository.All().Where(x => x.Id == fightId)
               .To<T>().FirstOrDefault();

            return fight;
        }
    }
}
