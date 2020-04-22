namespace PugnaFighting.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using PugnaFighting.Data.Models;

    public interface IBiographiesService
    {
        Task<int> CreateAsync(string firstName, string nickname, string lastName, string bornCountry, int age, IFormFile picture);

        public Biography GetById(int id);

        public Task DeleteAsync(int id);
    }
}
