namespace PugnaFighting.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Services.Data;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Cutmen;
    using PugnaFighting.Web.ViewModels.Fighters;

    [Authorize]
    public class CutmenController : Controller
    {
        private readonly ICutmenService cutmenService;
        private readonly IFightersService fightersService;

        public CutmenController(ICutmenService cutmenService, IFightersService fightersService)
        {
            this.cutmenService = cutmenService;
            this.fightersService = fightersService;
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

        public IActionResult Details(int id)
        {
            var fighters = this.fightersService.GetAllFightersWithoutCutmen<FightersDropDownViewModel>();
            var cutmanViewModel = this.cutmenService.GetById<DetailsCutmanViewModel>(id);

            cutmanViewModel.Fighters = fighters;

            if (cutmanViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(cutmanViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AppointCutmanToFighter(DetailsCutmanViewModel cutmanViewModel)
        {
            var fighter = this.fightersService.GetById(cutmanViewModel.FighterId);

            await this.fightersService.AppointCutmanToFighter(fighter, cutmanViewModel.Id);

            return this.RedirectToAction("AllFighters", "Users");
        }
    }
}
