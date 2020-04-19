namespace PugnaFighting.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IRecordsService
    {
        Task<int> CreateAsync();
    }
}
