namespace PugnaFighting.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Coaches;
    using PugnaFighting.Web.ViewModels.Fighters;

    [Authorize]
    public class CoachesController : Controller
    {
        private readonly ICoachesService coachesService;
        private readonly IFightersService fightersService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoachesController(
            ICoachesService coachesService,
            IFightersService fightersService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.coachesService = coachesService;
            this.fightersService = fightersService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var coaches = this.coachesService.GetAll<CoachViewModel>();

            var viewModel = new AllCoachesViewModel
            {
                CoachViewModels = coaches,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var fighters = this.fightersService.GetAllFightersWithoutCoaches<FightersDropDownViewModel>(user.Id);
            var coachViewModel = this.coachesService.GetById<DetailsCoachViewModel>(id);

            if (coachViewModel == null)
            {
                return this.NotFound();
            }

            coachViewModel.Fighters = fighters;
            coachViewModel.User = user;

            return this.View(coachViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AppointCoachToFighter(DetailsCoachViewModel coachViewModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var coach = this.coachesService.GetById<DetailsCoachViewModel>(coachViewModel.Id);
            var fighter = this.fightersService.GetById(coachViewModel.FighterId);

            await this.coachesService.AppointCoachToFighterAsync(fighter, coachViewModel.Id);
            await this.usersService.PayForNewTeamMemberAsync(user, coach.Price);

            return this.RedirectToAction("AllFighters", "Users");
        }
    }
}
