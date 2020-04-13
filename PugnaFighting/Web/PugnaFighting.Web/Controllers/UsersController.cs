namespace PugnaFighting.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Fighters;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
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
    }
}
