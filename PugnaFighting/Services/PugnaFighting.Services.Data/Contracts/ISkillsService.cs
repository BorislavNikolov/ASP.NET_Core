namespace PugnaFighting.Services.Data
{
    using System.Threading.Tasks;

    public interface ISkillsService
    {
        Task<int> CreateAsync();
    }
}
