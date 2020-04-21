namespace PugnaFighting.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Services.Data;
    using PugnaFighting.Web.ViewModels;
    using PugnaFighting.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private const string AnimoOrganizationName = "Animo";
        private const string CorporisOrganizationName = "Corporis";
        private readonly IFightersService fightersService;

        public HomeController(IFightersService fightersService)
        {
            this.fightersService = fightersService;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated == false)
            {
                return this.Redirect("/Home/IndexGuest");
            }

            var animoBestStriker = this.fightersService.GetBestStriker<BestFighterViewModel>(AnimoOrganizationName);
            var animoBestGrapller = this.fightersService.GetBestGrappler<BestFighterViewModel>(AnimoOrganizationName);
            var animoBestWrestller = this.fightersService.GetBestWrestler<BestFighterViewModel>(AnimoOrganizationName);

            var corporisBestStriker = this.fightersService.GetBestStriker<BestFighterViewModel>(CorporisOrganizationName);
            var corporisBestGrappler = this.fightersService.GetBestGrappler<BestFighterViewModel>(CorporisOrganizationName);
            var corporisBestWrestller = this.fightersService.GetBestWrestler<BestFighterViewModel>(CorporisOrganizationName);

            var viewModel = new IndexViewModel
            {
                AnimoGrapller = animoBestGrapller,
                AnimoStriker = animoBestStriker,
                AnimoWrestler = animoBestWrestller,
                CorporisGrapller = corporisBestGrappler,
                CorporisStriker = corporisBestStriker,
                CorporisWrestler = corporisBestWrestller,
            };

            return this.View(viewModel);
        }

        public IActionResult HttpError(int statusCode)
        {
            if (statusCode == 404)
            {
                return this.View("NotFound", statusCode);
            }

            return this.View("Error");
        }

        [Authorize]
        public IActionResult NotEnoughCoins()
        {
            return this.View();
        }

        [AllowAnonymous]
        public IActionResult IndexGuest()
        {
            return this.View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
