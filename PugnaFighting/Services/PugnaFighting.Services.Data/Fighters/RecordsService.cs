namespace PugnaFighting.Services.Data.Fighters
{
<<<<<<< HEAD
=======
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
>>>>>>> remotes/origin/master
    using PugnaFighting.Services.Data.Contracts;

    public class RecordsService : IRecordsService
    {
<<<<<<< HEAD
=======
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
>>>>>>> remotes/origin/master
    }
}
