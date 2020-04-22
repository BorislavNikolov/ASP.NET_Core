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

            await this.managersService.AppointManagerToFighterAsync(fighter, managerViewModel.Id);
            await this.usersService.PayForNewTeamMemberAsync(user, manager.Price);

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
            if (!this.ModelState.IsValid)
            {
                var userId = this.userManager.GetUserId(this.User);
                var fighters = this.fightersService.GetAllFightersWithoutManagers<FightersDropDownViewModel>(userId);
                input.Fighters = fighters;

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            // Give parameters
            var managerId = await this.managersService.CreateAsync(input);
            var fighter = this.fightersService.GetById(input.FighterId);
            await this.managersService.AppointManagerToFighterAsync(fighter, managerId);
            await this.usersService.PayForNewTeamMemberAsync(user, input.Price);

            this.TempData["InfoMessage"] = "Manager created!";

            return this.RedirectToAction("AllFighters", "Users");
        }
    }
}
