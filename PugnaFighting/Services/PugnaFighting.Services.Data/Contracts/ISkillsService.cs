namespace PugnaFighting.Services.Data
{
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface ISkillsService
    {
        Task<int> CreateAsync();

        public Skill GetById(int id);

        public Task Delete(int id);
    }
}
