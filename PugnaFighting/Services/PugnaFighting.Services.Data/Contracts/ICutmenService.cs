﻿namespace PugnaFighting.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public interface ICutmenService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);

        Task AppointCutmanToFighterAsync(Fighter fighter, int cutmanId);

        Task FireCutmanAsync(Fighter fighter);
    }
}
