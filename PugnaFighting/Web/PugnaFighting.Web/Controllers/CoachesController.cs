namespace PugnaFighting.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PugnaFighting.Services.Data;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Coaches;
    using PugnaFighting.Web.ViewModels.Fighters;

    [Authorize]
    public class CoachesController : Controller
    {
        private readonly ICoachesService coachesService;
        private readonly IFightersService fightersService;

        public CoachesController(ICoachesService coachesService, IFightersService fightersService)
        {
            this.coachesService = coachesService;
            this.fightersService = fightersService;
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

        public IActionResult Details(int id)
        {
            var fighters = this.fightersService.GetAllFightersWithoutCoaches<FightersDropDownViewModel>();
            var coachViewModel = this.coachesService.GetById<DetailsCoachViewModel>(id);

            coachViewModel.Fighters = fighters;

            if (coachViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(coachViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AppointCoachToFighter(DetailsCoachViewModel coachViewModel)
        {
            var fighter = this.fightersService.GetById(coachViewModel.FighterId);

            await this.fightersService.AppointCoachToFighter(fighter, coachViewModel.Id);

            return this.RedirectToAction("AllFighters", "Users");
        }
    }
}
