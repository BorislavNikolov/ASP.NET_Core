namespace PugnaFighting.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Web.ViewModels.Managers;

    public interface IManagersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);

        Task<int> CreateAsync(CreateManagerViewModel viewModel);

        Task AppointManagerToFighterAsync(Fighter fighter, int managerId);

        Task FireManagerAsync(Fighter fighter);
    }
}
