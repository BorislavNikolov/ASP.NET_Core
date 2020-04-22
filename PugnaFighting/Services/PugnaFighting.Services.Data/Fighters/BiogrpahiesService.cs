namespace PugnaFighting.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;

    public class BiogrpahiesService : IBiographiesService
    {
        private const string CloudinaryFolderName = "FightersPics";
        private const string UnknownPictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1586529578/FightersPics/Unknown_olk6sa.jpg";

        private readonly IDeletableEntityRepository<Biography> biographiesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public BiogrpahiesService(
            IDeletableEntityRepository<Biography> biographiesRepository,
            ICloudinaryService cloudinaryService)
        {
            this.biographiesRepository = biographiesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<int> CreateAsync(string firstName, string nickname, string lastName, string bornCountry, int age, IFormFile picture)
        {
            var biography = new Biography
            {
                FirstName = firstName,
                Nickname = nickname,
                LastName = lastName,
                BornCountry = bornCountry,
                Age = age,
            };

            if (picture != null)
            {
                var pictureUrl = await this.cloudinaryService.UploadAsync(picture, picture.FileName, CloudinaryFolderName);

                biography.PictureUrl = pictureUrl;
            }
            else
            {
                biography.PictureUrl = UnknownPictureUrl;
            }

            await this.biographiesRepository.AddAsync(biography);
            await this.biographiesRepository.SaveChangesAsync();

            return biography.Id;
        }

        public Biography GetById(int id)
        {
            var biography = this.biographiesRepository.All().Where(x => x.Id == id).FirstOrDefault();
            return biography;
        }

        public async Task DeleteAsync(int id)
        {
            var biography = this.GetById(id);

            biography.IsDeleted = true;
            biography.DeletedOn = DateTime.UtcNow;

            await this.biographiesRepository.SaveChangesAsync();
        }
    }
}
