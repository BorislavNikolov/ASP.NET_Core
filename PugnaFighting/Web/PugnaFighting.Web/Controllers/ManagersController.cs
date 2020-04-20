namespace PugnaFighting.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Fighters;
    using PugnaFighting.Web.ViewModels.Managers;

    [Authorize]
    public class ManagersController : Controller
    {
        private const int DefaultCustomManagerPrice = 20000;

        private readonly IManagersService managersService;
        private readonly IFightersService fightersService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ManagersController(
            IManagersService managersService,
            IFightersService fightersServie,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.managersService = managersService;
            this.fightersService = fightersServie;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var managers = this.managersService.GetAll<ManagerViewModel>();
            var fighters = this.fightersService.GetAllFightersWithoutManagers<FightersDropDownViewModel>(user.Id);

            var viewModel = new AllManagersViewModel
            {
                ManagerViewModels = managers,
                Fighters = fighters,
                User = user,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var fighters = this.fightersService.GetAllFightersWithoutManagers<FightersDropDownViewModel>(user.Id);
            var managerViewModel = this.managersService.GetById<DetailsManagerViewModel>(id);

            if (managerViewModel == null)
            {
                return this.NotFound();
            }

            managerViewModel.Fighters = fighters;
            managerViewModel.User = user;

            return this.View(managerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AppointManagerToFighter(DetailsManagerViewModel managerViewModel)
        {
            var manager = this.managersService.GetById<DetailsManagerViewModel>(managerViewModel.Id);
            var user = await this.userManager.GetUserAsync(this.User);
            var fighter = this.fightersService.GetById(managerViewModel.FighterId);

            await this.fightersService.AppointManagerToFighter(fighter, managerViewModel.Id);
            await this.usersService.PayForNewTeamMember(user, manager.Price);

            return this.RedirectToAction("AllFighters", "Users");
        }

        public IActionResult Create()
        {
            var userId = this.userManager.GetUserId(this.User);

            var fighters = this.fightersService.GetAllFightersWithoutManagers<FightersDropDownViewModel>(userId);

            var viewModel = new CreateManagerViewModel
            {
                Fighters = fighters,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateManagerViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            input.Price = DefaultCustomManagerPrice;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // Give parameters
            var managerId = await this.managersService.CreateAsync(input);
            var fighter = this.fightersService.GetById(input.FighterId);
            await this.fightersService.AppointManagerToFighter(fighter, managerId);
            await this.usersService.PayForNewTeamMember(user, input.Price);

            this.TempData["InfoMessage"] = "Manager created!";

            return this.RedirectToAction("AllFighters", "Users");
        }
    }
}
