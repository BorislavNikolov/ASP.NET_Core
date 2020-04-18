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
                    OrganizationId = 1,
                    SkillId = 1,
                    BiographyId = 1,
                    RecordId = 1,
                    CategoryId = 5,
                    UserId = "f6c3ad4e-3789-43ab-9685-027d03c21101",
                },
                new Fighter
                {
                    FansCount = 100,
                    MoneyPerFight = 100,
                    OrganizationId = 1,
                    SkillId = 2,
                    BiographyId = 2,
                    RecordId = 1,
                    CategoryId = 1,
                    UserId = "f6c3ad4e-3789-43ab-9685-027d03c21101",
                },
                new Fighter
                {
                    FansCount = 100,
                    MoneyPerFight = 100,
                    OrganizationId = 1,
                    SkillId = 3,
                    BiographyId = 3,
                    RecordId = 1,
                    CategoryId = 2,
                    UserId = "f6c3ad4e-3789-43ab-9685-027d03c21101",
                },
                new Fighter
                {
                    FansCount = 100,
                    MoneyPerFight = 100,
                    OrganizationId = 2,
                    SkillId = 3,
                    BiographyId = 2,
                    RecordId = 1,
                    CategoryId = 3,
                    UserId = "f6c3ad4e-3789-43ab-9685-027d03c21101",
                },
                new Fighter
                {
                    FansCount = 100,
                    MoneyPerFight = 100,
                    OrganizationId = 2,
                    SkillId = 2,
                    BiographyId = 1,
                    RecordId = 1,
                    CategoryId = 4,
                    UserId = "f6c3ad4e-3789-43ab-9685-027d03c21101",
                },
                new Fighter
                {
                    FansCount = 100,
                    MoneyPerFight = 100,
                    OrganizationId = 2,
                    SkillId = 1,
                    BiographyId = 3,
                    RecordId = 1,
                    CategoryId = 5,
                    UserId = "f6c3ad4e-3789-43ab-9685-027d03c21101",
                },
            };

            foreach (var fighter in fighters)
            {
                await dbContext.Fighters.AddAsync(fighter);
            }
        }
    }
}
