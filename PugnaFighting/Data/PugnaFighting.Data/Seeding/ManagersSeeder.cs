namespace PugnaFighting.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public class ManagersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Managers.Any())
            {
                return;
            }

            var managers = new List<Manager>
            {
                new Manager
                {
                    FirstName = "Christopher",
                    LastName = "Bennett",
                    Age = 24,
                    BornCountry = "America",
                    Price = 10000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585939328/face13_rd8j7r.png",
                    FansCount = 100,
                    MoneyPerFight = 700,
                    IsCustom = false,
                },
                new Manager
                {
                    FirstName = "Robert",
                    LastName = "Thompson",
                    Age = 30,
                    BornCountry = "England",
                    Price = 20000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585939332/face24_taldsx.png",
                    FansCount = 200,
                    MoneyPerFight = 1500,
                    IsCustom = false,
                },
                new Manager
                {
                    FirstName = "James",
                    LastName = "Lopez",
                    Age = 37,
                    BornCountry = "Spain",
                    Price = 30000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585940165/face27_qzx7sq.png",
                    FansCount = 300,
                    MoneyPerFight = 2000,
                    IsCustom = false,
                },
                new Manager
                {
                    FirstName = "John",
                    LastName = "Sullivan",
                    Age = 48,
                    BornCountry = "Ireland",
                    Price = 40000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585938629/face9_pcg4u5.png",
                    FansCount = 400,
                    MoneyPerFight = 2500,
                    IsCustom = false,
                },
                new Manager
                {
                    FirstName = "Boris",
                    LastName = "Alexandrov",
                    Age = 55,
                    BornCountry = "Russia",
                    Price = 50000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585938628/face8_pfqvgw.png",
                    FansCount = 500,
                    MoneyPerFight = 3000,
                    IsCustom = false,
                },
            };

            foreach (var manager in managers)
            {
                await dbContext.Managers.AddAsync(manager);
            }
        }
    }
}
