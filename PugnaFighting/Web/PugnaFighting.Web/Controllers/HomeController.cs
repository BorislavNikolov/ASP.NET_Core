namespace PugnaFighting.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using PugnaFighting.Web.ViewModels;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated == false)
            {
                return this.Redirect("/Home/IndexGuest");
            }

            return this.View();
        }

        public IActionResult HttpError(int statusCode)
        {
            if (statusCode == 404)
            {
                return this.View("NotFound", statusCode);
            }

            return this.View("Error");
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
