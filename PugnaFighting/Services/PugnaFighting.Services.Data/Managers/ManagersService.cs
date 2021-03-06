﻿namespace PugnaFighting.Services.Data.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PugnaFighting.Data.Common.Repositories;
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.ViewModels.Managers;

    public class ManagersService : IManagersService
    {
        private const int DefaultCustomManagerPrice = 20000;
        private const string CloudinaryFolderName = "FightersPics";

        private const string UnknownPictureUrl = "https://res.cloudinary.com/dka5uzl0n/image/upload/v1586529578/FightersPics/Unknown_olk6sa.jpg";
        private readonly IDeletableEntityRepository<Manager> managersRepository;
        private readonly IDeletableEntityRepository<Fighter> fightersRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ManagersService(
            IDeletableEntityRepository<Manager> managersRepository,
            IDeletableEntityRepository<Fighter> fightersRepository,
            ICloudinaryService cloudinaryService)
        {
            this.managersRepository = managersRepository;
            this.fightersRepository = fightersRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Manager> query =
                this.managersRepository.All().Where(x => x.IsCustom == false).OrderBy(x => x.Price);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var manager = this.managersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return manager;
        }

        public async Task<int> CreateAsync(CreateManagerViewModel viewModel)
        {
            var manager = new Manager
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                BornCountry = viewModel.BornCountry,
                Age = viewModel.Age,
                Price = DefaultCustomManagerPrice,
                MoneyPerFight = viewModel.MoneyPerFight,
                FansCount = viewModel.FansCount,
                IsCustom = true,
            };

            if (viewModel.Picture != null)
            {
                var pictureUrl = await this.cloudinaryService.UploadAsync(viewModel.Picture, viewModel.Picture.FileName, CloudinaryFolderName);

                manager.PictureUrl = pictureUrl;
            }
            else
            {
                manager.PictureUrl = UnknownPictureUrl;
            }

            await this.managersRepository.AddAsync(manager);
            await this.managersRepository.SaveChangesAsync();

            return manager.Id;
        }

        public async Task AppointManagerToFighterAsync(Fighter fighter, int managerId)
        {
            var manager = this.GetById<DetailsManagerViewModel>(managerId);

            fighter.ManagerId = managerId;
            fighter.MoneyPerFight += manager.MoneyPerFight;
            fighter.FansCount += manager.FansCount;

            await this.fightersRepository.SaveChangesAsync();
        }

        public async Task FireManagerAsync(Fighter fighter)
        {
            var manager = this.GetById<DetailsManagerViewModel>(int.Parse(fighter.ManagerId.ToString()));

            fighter.ManagerId = null;
            fighter.Manager = null;
            fighter.FansCount = fighter.FansCount - manager.FansCount < 0 ? 0 : fighter.FansCount - manager.FansCount;
            fighter.MoneyPerFight = fighter.MoneyPerFight - manager.MoneyPerFight < 0 ? 0 : fighter.MoneyPerFight - manager.MoneyPerFight;

            await this.fightersRepository.SaveChangesAsync();
        }
    }
}
