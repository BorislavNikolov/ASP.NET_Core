namespace PugnaFighting.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface ICoachesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);

        Task AppointCoachToFighterAsync(Fighter fighter, int coachId);

        Task FireCoachAsync(Fighter fighter);
    }
}
