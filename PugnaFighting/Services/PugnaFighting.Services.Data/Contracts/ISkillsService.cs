namespace PugnaFighting.Services.Data
{
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Web.ViewModels.Skills;

    public interface ISkillsService
    {
        Task<int> CreateAsync();

        Skill GetById(int id);

        Task DeleteAsync(int id);

        T GetById<T>(int id);

        Task UpdateSkillPointsAsync(TrainViewModel trainViewModel, int skillId);

        int GetSkillPointsOverall(Skill skill);

        bool ChechForEnoughCoinsToTrain(ApplicationUser user, Skill skill, TrainViewModel trainViewModel);
    }
}
