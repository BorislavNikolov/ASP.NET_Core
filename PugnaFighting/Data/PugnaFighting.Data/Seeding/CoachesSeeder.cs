namespace PugnaFighting.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public class CoachesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Coaches.Any())
            {
                return;
            }

            var coaches = new List<Coach>
            {
                new Coach
                {
                    FirstName = "Christopher",
                    LastName = "Colon",
                    Age = 33,
                    BornCountry = "England",
                    Price = 7000,
                    SkillBonus = 1,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585938627/face6_pfqgw6.png",
                    IsCustom = false,
                },
                new Coach
                {
                    FirstName = "David",
                    LastName = "Franco",
                    Age = 25,
                    BornCountry = "Italy",
                    Price = 14000,
                    SkillBonus = 2,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585938629/face7_jadn9s.png",
                    IsCustom = false,
                },
                new Coach
                {
                    FirstName = "James",
                    LastName = "Shields",
                    Age = 40,
                    BornCountry = "America",
                    Price = 21000,
                    SkillBonus = 3,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585939331/face23_szo3ed.png",
                    IsCustom = false,
                },
                new Coach
                {
                    FirstName = "Drago",
                    LastName = "Volkov",
                    Age = 52,
                    BornCountry = "Russia",
                    Price = 28000,
                    SkillBonus = 4,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585938627/face5_f6gznz.png",
                    IsCustom = false,
                },
                new Coach
                {
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Age = 66,
                    BornCountry = "Bulgaria",
                    Price = 35000,
                    SkillBonus = 5,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585939327/face12_v3joed.png",
                    IsCustom = false,
                },
            };

            foreach (var coach in coaches)
            {
                await dbContext.Coaches.AddAsync(coach);
            }
        }
    }
}
