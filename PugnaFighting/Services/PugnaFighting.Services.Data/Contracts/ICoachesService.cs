﻿namespace PugnaFighting.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ICoachesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);
    }
}
