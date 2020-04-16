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
    using PugnaFighting.Web.ViewModels.Cutmen;
    using PugnaFighting.Web.ViewModels.Fighters;
    using PugnaFighting.Web.ViewModels.Managers;
    using PugnaFighting.Web.ViewModels.Skills;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ISkillsService skillsService;
        private readonly IFightersService fightersService;
        private readonly IBiographiesService biographiesService;
        private readonly IManagersService managersService;
        private readonly ICoachesService coachesService;
        private readonly ICutmenService cutmenService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager,
            IFightersService fightersService,
            ISkillsService skillsService,
            IBiographiesService biographiesService,
            IManagersService managersService,
            ICoachesService coachesService,
            ICutmenService cutmenService)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.fightersService = fightersService;
            this.skillsService = skillsService;
            this.biographiesService = biographiesService;
            this.managersService = managersService;
            this.coachesService = coachesService;
            this.cutmenService = cutmenService;
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

        public IActionResult ManagerDetails()
        {
            var fighterId = int.Parse(this.RouteData.Values["id"].ToString());
            var fighter = this.fightersService.GetById(fighterId);

            var managerId = int.Parse(fighter.ManagerId.ToString());

            var managerViewModel = this.managersService.GetById<DetailsManagerViewModel>(managerId);
            managerViewModel.FighterId = fighterId;

            return this.View(managerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FireManager()
        {
            var fighterId = int.Parse(this.RouteData.Values["id"].ToString());

            var fighter = this.fightersService.GetById(fighterId);

            await this.fightersService.FireManager(fighter);

            return this.RedirectToAction(nameof(this.AllFighters));
        }

        public IActionResult CoachDetails()
        {
            var fighterId = int.Parse(this.RouteData.Values["id"].ToString());
            var fighter = this.fightersService.GetById(fighterId);

            var coachId = int.Parse(fighter.CoachId.ToString());

            var coachViewModel = this.coachesService.GetById<DetailsCoachViewModel>(coachId);
            coachViewModel.FighterId = fighterId;

            return this.View(coachViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FireCoach()
        {
            var fighterId = int.Parse(this.RouteData.Values["id"].ToString());

            var fighter = this.fightersService.GetById(fighterId);

            await this.fightersService.FireCoach(fighter);

            return this.RedirectToAction(nameof(this.AllFighters));
        }

        public IActionResult CutmanDetails()
        {
            var fighterId = int.Parse(this.RouteData.Values["id"].ToString());
            var fighter = this.fightersService.GetById(fighterId);

            var cutmanId = int.Parse(fighter.CutmanId.ToString());

            var cutmanViewModel = this.cutmenService.GetById<DetailsCutmanViewModel>(cutmanId);
            cutmanViewModel.FighterId = fighterId;

            return this.View(cutmanViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FireCutman()
        {
            var fighterId = int.Parse(this.RouteData.Values["id"].ToString());

            var fighter = this.fightersService.GetById(fighterId);

            await this.fightersService.FireCutman(fighter);

            return this.RedirectToAction(nameof(this.AllFighters));
        }
    }
}
