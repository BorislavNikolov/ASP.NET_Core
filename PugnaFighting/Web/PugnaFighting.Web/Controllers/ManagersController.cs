namespace PugnaFighting.Web.Controllers
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
        private readonly IFightersService fightersService;

        public ManagersController(IManagersService managersService, IFightersService fightersServie)
        {
            this.managersService = managersService;
            this.fightersService = fightersServie;
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
            var fighters = this.fightersService.GetAllFightersWithoutManagers<FightersDropDownViewModel>();
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
            var fighter = this.fightersService.GetById(managerViewModel.FighterId);

            await this.fightersService.AppointManagerToFighter(fighter, managerViewModel.Id);

            return this.RedirectToAction("AllFighters", "Users");
        }

        public IActionResult Create()
        {
            var fighters = this.fightersService.GetAllFightersWithoutManagers<FightersDropDownViewModel>();

            var viewModel = new CreateManagerViewModel
            {
                Fighters = fighters,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateManagerViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // Give parameters
            var managerId = await this.managersService.CreateAsync(input);
            this.TempData["InfoMessage"] = "Manager created!";

            var fighter = this.fightersService.GetById(input.FighterId);
            await this.fightersService.AppointManagerToFighter(fighter, managerId);

            return this.RedirectToAction("AllFighters", "Users");
        }
    }
}
