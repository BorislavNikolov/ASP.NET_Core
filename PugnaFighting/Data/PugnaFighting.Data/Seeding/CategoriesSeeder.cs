namespace PugnaFighting.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Flyweight",
                    WeightLimit = "56.7 kg",
                },
                new Category
                {
                    Name = "Lightweight",
                    WeightLimit = "70.3 kg",
                },
                new Category
                {
                    Name = "Welterweight",
                    WeightLimit = "77.1 kg",
                },
                new Category
                {
                    Name = "Middleweight",
                    WeightLimit = "83.9 kg",
                },
                new Category
                {
                    Name = "Heavyweight",
                    WeightLimit = "120.2 kg",
                },
            };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(category);
            }
        }
    }
}
