namespace PugnaFighting.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Cutmen;

    [Authorize]
    public class CutmenController : Controller
    {
        private readonly ICutmenService cutmenService;

        public CutmenController(ICutmenService cutmenService)
        {
            this.cutmenService = cutmenService;
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
    }
}
