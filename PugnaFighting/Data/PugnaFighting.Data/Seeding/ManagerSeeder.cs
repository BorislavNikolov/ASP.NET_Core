namespace PugnaFighting.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public class ManagerSeeder : ISeeder
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
                    Nationality = "American",
                    Price = 10000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585939328/face13_rd8j7r.png",
                },
                new Manager
                {
                    FirstName = "Robert",
                    LastName = "Thompson",
                    Age = 30,
                    Nationality = "Englishman",
                    Price = 20000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585939332/face24_taldsx.png",
                },
                new Manager
                {
                    FirstName = "James",
                    LastName = "Lopez",
                    Age = 37,
                    Nationality = "Spaniard",
                    Price = 30000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585940165/face27_qzx7sq.png",
                },
                new Manager
                {
                    FirstName = "John",
                    LastName = "Sullivan",
                    Age = 48,
                    Nationality = "Irishman",
                    Price = 40000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585938629/face7_jadn9s.png",
                },
                new Manager
                {
                    FirstName = "Boris",
                    LastName = "Alexandrov",
                    Age = 55,
                    Nationality = "Russian",
                    Price = 50000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585938628/face8_pfqvgw.png",
                },
            };

            foreach (var manager in managers)
            {
                await dbContext.Managers.AddAsync(manager);
            }
        }
    }
}
