namespace PugnaFighting.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface IFightersService
    {
        Task<int> CreateAsync(int skillId, int biographyId, int recordId, int categoryId, ApplicationUser user);

        Fighter GetById(int id);

        public T GetById<T>(int id);

        Task SetOrganization(int fighterId, int organizationId, ApplicationUser user);

        T GetBestStriker<T>(string organizationName);

        T GetBestWrestler<T>(string organizationName);

        T GetBestGrappler<T>(string organizationName);

        IEnumerable<T> GetAllFightersWithoutManagers<T>(string userId);

        IEnumerable<T> GetAllFightersWithoutCoaches<T>(string userId);

        IEnumerable<T> GetAllFightersWithoutCutmen<T>(string userId);

        Task AppointManagerToFighter(Fighter fighter, int managerId);

        Task FireManager(Fighter fighter);

        Task AppointCoachToFighter(Fighter fighter, int coachId);

        Task FireCoach(Fighter fighter);

        Task AppointCutmanToFighter(Fighter fighter, int cutmanId);

        Task FireCutman(Fighter fighter);

        IEnumerable<T> GetAllOpponents<T>(string userId, int? take = null, int skip = 0);

        int GetOpponentsCount(string userId);

        Task<Fight> Fight(Fighter fighter, Fighter opponet);

        Task AddFightToRecord(Fight fight, Fighter fighter);

        public Record GetRecordById(int id);
    }
}
