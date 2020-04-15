namespace PugnaFighting.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public class FightersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Fighters.Any())
            {
                return;
            }

            var fighters = new List<Fighter>
            {
                new Fighter
                {
                    FansCount = 100,
                    MoneyPerFight = 100,
                    OrganizationId = 6,
                    SkillId = 1016,
                    BiographyId = 1010,
                    CategoryId = 5,
                    UserId = "e2f62228-1cfc-4409-bcc5-8fb6302dca5c",
                },
            };

            foreach (var fighter in fighters)
            {
                await dbContext.Fighters.AddAsync(fighter);
            }
        }
    }
}
