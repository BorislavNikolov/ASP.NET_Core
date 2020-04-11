namespace PugnaFighting.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IManagersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);
    }
}
