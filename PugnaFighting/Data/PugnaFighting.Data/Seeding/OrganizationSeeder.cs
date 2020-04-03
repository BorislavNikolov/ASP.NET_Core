namespace PugnaFighting.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public class OrganizationSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Organizations.Any())
            {
                return;
            }

            var organizations = new List<Organization>
            {
                new Organization
                {
                    Location = "North America",
                    Name = "Animo",
                    LogoUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585929571/Animo_ks4vtt.png",
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                },

                new Organization
                {
                    Location = "Europe",
                    Name = "Corporis",
                    LogoUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1585929571/Corporis_kall18.png",
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                },
            };

            foreach (var organization in organizations)
            {
                await dbContext.Organizations.AddAsync(organization);
            }
        }
    }
}
