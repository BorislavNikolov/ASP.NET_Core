namespace PugnaFighting.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public class CutmenSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cutmen.Any())
            {
                return;
            }

            var cutmen = new List<Cutman>
            {
                new Cutman
                {
                    FirstName = "Paul",
                    LastName = "Baker",
                    Age = 44,
                    BornCountry = "England",
                    Price = 5000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585939328/face14_qkppms.png",
                    HealthBonus = 1,
                    IsCustom = false,
                },
                new Cutman
                {
                    FirstName = "David",
                    LastName = "Carter",
                    Age = 32,
                    BornCountry = "America",
                    Price = 15000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585939330/face20_zozn7u.png",
                    HealthBonus = 2,
                    IsCustom = false,
                },
                new Cutman
                {
                    FirstName = "Joseph",
                    LastName = "Johnson",
                    Age = 35,
                    BornCountry = "England",
                    Price = 20000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585939330/face21_ns1ph7.png",
                    HealthBonus = 3,
                    IsCustom = false,
                },
                new Cutman
                {
                    FirstName = "Andrew",
                    LastName = "Rogers",
                    Age = 53,
                    BornCountry = "America",
                    Price = 25000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585940167/face32_q0ofhx.png",
                    HealthBonus = 4,
                    IsCustom = false,
                },
                new Cutman
                {
                    FirstName = "Michael",
                    LastName = "Sanders",
                    Age = 60,
                    BornCountry = "Mexico",
                    Price = 30000,
                    PictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585940169/face29_eko6il.png",
                    HealthBonus = 5,
                    IsCustom = false,
                },
            };

            foreach (var cutman in cutmen)
            {
                await dbContext.Cutmen.AddAsync(cutman);
            }
        }
    }
}
