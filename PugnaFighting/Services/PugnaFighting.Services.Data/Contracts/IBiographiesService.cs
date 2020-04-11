namespace PugnaFighting.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IBiographiesService
    {
        Task<int> CreateAsync(string firstName, string nickname, string lastName, string bornCountry, int age, IFormFile picture);
    }
}
