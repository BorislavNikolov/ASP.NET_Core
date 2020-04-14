namespace PugnaFighting.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface IUsersService
    {
        IEnumerable<T> GetAllFighters<T>(string userId);

        Task<T> GetFighterById<T>(int id);

        Task DeleteFighter(Fighter fighter, ApplicationUser user);
    }
}
