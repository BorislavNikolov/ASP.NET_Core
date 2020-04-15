﻿namespace PugnaFighting.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Services.Data;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Fighters;
    using PugnaFighting.Web.ViewModels.Managers;

    [Authorize]
    public class ManagersController : Controller
    {
        private readonly IManagersService managersService;
        private readonly IFightersService fightersServie;

        public ManagersController(IManagersService managersService, IFightersService fightersServie)
        {
            this.managersService = managersService;
            this.fightersServie = fightersServie;
        }

        public IActionResult All()
        {
            var managers = this.managersService.GetAll<ManagerViewModel>();

            var viewModel = new AllManagersViewModel
            {
                ManagerViewModels = managers,
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var fighters = this.fightersServie.GetAll<FightersDropDownViewModel>();
            var managerViewModel = this.managersService.GetById<DetailsManagerViewModel>(id);

            managerViewModel.Fighters = fighters;

            if (managerViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(managerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AppointManagerToFighter(DetailsManagerViewModel managerViewModel)
        {
            var fighter = this.fightersServie.GetById(managerViewModel.FighterId);

            await this.fightersServie.AppointManagerToFighter(fighter, managerViewModel.Id);

            return this.RedirectToAction("AllFighters", "Users");
        }
    }
}
