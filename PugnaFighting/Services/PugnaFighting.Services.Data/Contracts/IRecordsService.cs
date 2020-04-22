namespace PugnaFighting.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface IRecordsService
    {
        Task<int> CreateAsync();

        Record GetById(int id);

        T GetFight<T>(int fightId);
    }
}
