namespace PugnaFighting.Services.Data
{
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Web.ViewModels.Skills;

    public interface ISkillsService
    {
        Task<int> CreateAsync();

        Skill GetById(int id);

        Task Delete(int id);

        T GetById<T>(int id);

        Task UpdateSkillPoints(TrainViewModel trainViewModel, int skillId);
    }
}
