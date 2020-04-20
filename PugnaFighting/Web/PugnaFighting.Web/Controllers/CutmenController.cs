namespace PugnaFighting.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Cutmen;
    using PugnaFighting.Web.ViewModels.Fighters;

    [Authorize]
    public class CutmenController : Controller
    {
        private readonly ICutmenService cutmenService;
        private readonly IFightersService fightersService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public CutmenController(
            ICutmenService cutmenService,
            IFightersService fightersService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.cutmenService = cutmenService;
            this.fightersService = fightersService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var cutmen = this.cutmenService.GetAll<CutmanViewModel>();

            var viewModel = new AllCutmenViewModel
            {
                CutmanViewModels = cutmen,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var fighters = this.fightersService.GetAllFightersWithoutCutmen<FightersDropDownViewModel>(user.Id);
            var cutmanViewModel = this.cutmenService.GetById<DetailsCutmanViewModel>(id);

            if (cutmanViewModel == null)
            {
                return this.NotFound();
            }

            cutmanViewModel.Fighters = fighters;
            cutmanViewModel.User = user;

            return this.View(cutmanViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AppointCutmanToFighter(DetailsCutmanViewModel cutmanViewModel)
        {
            var cutman = this.cutmenService.GetById<DetailsCutmanViewModel>(cutmanViewModel.Id);
            var user = await this.userManager.GetUserAsync(this.User);
            var fighter = this.fightersService.GetById(cutmanViewModel.FighterId);

            await this.fightersService.AppointCutmanToFighter(fighter, cutmanViewModel.Id);
            await this.usersService.PayForNewTeamMember(user, cutman.Price);

            return this.RedirectToAction("AllFighters", "Users");
        }
    }
}
