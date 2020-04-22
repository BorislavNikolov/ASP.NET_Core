namespace PugnaFighting.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface IUsersService
    {
        IEnumerable<T> GetAllFighters<T>(string userId);

        Task<T> GetFighterByIdAsync<T>(int id);

        Task DeleteFighterAsync(Fighter fighter, ApplicationUser user);

        Task PayForNewFighterAsync(ApplicationUser user);

        Task PayForNewTeamMemberAsync(ApplicationUser user, int price);
    }
}
