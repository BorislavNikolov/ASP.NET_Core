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
    using PugnaFighting.Web.ViewModels.Skills;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ISkillsService skillsService;
        private readonly IFightersService fightersService;
        private readonly IBiographiesService biographiesService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager,
            IFightersService fightersService,
            ISkillsService skillsService,
            IBiographiesService biographiesService)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.fightersService = fightersService;
            this.skillsService = skillsService;
            this.biographiesService = biographiesService;
        }

        public IActionResult AllFighters()
        {
            var userId = this.userManager.GetUserId(this.User);
            var fighters = this.usersService.GetAllFighters<FighterViewModel>(userId);

            var viewModel = new AllFightersViewModel
            {
                FighterViewModels = fighters,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var fighterViewModel = await this.usersService.GetFighterById<DetailsFighterViewModel>(id);

            if (fighterViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(fighterViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SellFighter(int id)
        {
            var fighter = this.fightersService.GetById(id);
            var user = await this.userManager.GetUserAsync(this.User);

            await this.usersService.DeleteFighter(fighter, user);
            await this.biographiesService.Delete(fighter.BiographyId);
            await this.skillsService.Delete(fighter.SkillId);

            return this.RedirectToAction(nameof(this.AllFighters));
        }

        public IActionResult Train()
        {
            var skillId = int.Parse(this.RouteData.Values["id"].ToString());

            var skills = this.skillsService.GetById<TrainViewModel>(skillId);

            return this.View("Train", skills);
        }

        [HttpPost]
        public async Task<IActionResult> Train(TrainViewModel skills)
        {
            var skillId = int.Parse(this.RouteData.Values["id"].ToString());

            await this.skillsService.UpdateSkillPoints(skills, skillId);

            return this.RedirectToAction("AllFighters");
        }
    }
}
