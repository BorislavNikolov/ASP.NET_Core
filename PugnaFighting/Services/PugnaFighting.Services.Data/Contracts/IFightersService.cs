﻿namespace PugnaFighting.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface IFightersService
    {
        Task<int> CreateAsync(int skillId, int biographyId, int categoryId, ApplicationUser user);

        Fighter GetById(int id);

        Task SetOrganization(int fighterId, int organizationId, ApplicationUser user);

        T GetBestStriker<T>(string organizationName);

        T GetBestWrestler<T>(string organizationName);

        T GetBestGrappler<T>(string organizationName);

        IEnumerable<T> GetAllFightersWithoutManagers<T>();

        IEnumerable<T> GetAllFightersWithoutCoaches<T>();

        IEnumerable<T> GetAllFightersWithoutCutmen<T>();

        Task AppointManagerToFighter(Fighter fighter, int managerId);

        Task FireManager(Fighter fighter);

        Task AppointCoachToFighter(Fighter fighter, int coachId);

        Task FireCoach(Fighter fighter);

        Task AppointCutmanToFighter(Fighter fighter, int cutmanId);

        Task FireCutman(Fighter fighter);

        IEnumerable<T> GetAllOpponents<T>(string userId, int? take = null, int skip = 0);

        int GetOpponentsCount(string userId);
    }
}
