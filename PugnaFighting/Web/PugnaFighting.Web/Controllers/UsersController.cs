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

        public async Task<IActionResult> AllFighters()
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager.GetUserAsync(this.User);
            var fighters = this.usersService.GetAllFighters<FighterViewModel>(userId);

            var viewModel = new AllFightersViewModel
            {
                FighterViewModels = fighters,
                User = user,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int fighterId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var fighterViewModel = await this.usersService.GetFighterByIdAsync<DetailsFighterViewModel>(fighterId);
            var fighter = this.fightersService.GetById(fighterId);

            if (fighterViewModel == null || fighter.User != user)
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

            await this.usersService.DeleteFighterAsync(fighter, user);
            await this.biographiesService.DeleteAsync(fighter.BiographyId);
            await this.skillsService.DeleteAsync(fighter.SkillId);

            return this.RedirectToAction(nameof(this.AllFighters));
        }

        public IActionResult Train()
        {
            var skillId = int.Parse(this.RouteData.Values["id"].ToString());

            var skills = this.skillsService.GetById<TrainViewModel>(skillId);

            return this.View("Train", skills);
        }

        [HttpPost]
        public async Task<IActionResult> Train(TrainViewModel newSkills)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var skillId = int.Parse(this.RouteData.Values["id"].ToString());
            var skill = this.skillsService.GetById(skillId);

            var hasEnoughCoinsToMakeTheTraining = this.skillsService.ChechForEnoughCoinsToTrain(user, skill, newSkills);

            if (hasEnoughCoinsToMakeTheTraining == false)
            {
                return this.RedirectToAction("NotEnoughCoins", "Home");
            }

            await this.skillsService.UpdateSkillPointsAsync(newSkills, skillId);

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

            await this.managersService.FireManagerAsync(fighter);

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

            await this.coachesService.FireCoachAsync(fighter);

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

            await this.cutmenService.FireCutmanAsync(fighter);

            return this.RedirectToAction(nameof(this.AllFighters));
        }
    }
}
