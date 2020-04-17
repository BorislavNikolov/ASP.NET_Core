namespace PugnaFighting.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data;
    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Fighters;
    using PugnaFighting.Web.ViewModels.Organizations;

    [Authorize]
    public class FightersController : Controller
    {
        private const int ItemsPerPage = 5;

        private readonly IFightersService fightersService;
        private readonly ICategoriesService categoriesService;
        private readonly IBiographiesService biographiesService;
        private readonly ISkillsService skillsService;
        private readonly IOrganizationsService organizationsService;
        private readonly IUsersService usersService;
        private readonly IHttpContextAccessor http;
        private readonly UserManager<ApplicationUser> userManager;

        public FightersController(
            IFightersService fightersService,
            ICategoriesService categoriesService,
            IBiographiesService biographiesService,
            ISkillsService skillsService,
            IOrganizationsService organizationsService,
            IUsersService usersService,
            IHttpContextAccessor http,
            UserManager<ApplicationUser> userManager)
        {
            this.fightersService = fightersService;
            this.categoriesService = categoriesService;
            this.biographiesService = biographiesService;
            this.skillsService = skillsService;
            this.organizationsService = organizationsService;
            this.usersService = usersService;
            this.http = http;
            this.userManager = userManager;
        }

        public IActionResult All(int page = 1)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var opponents = this.fightersService.GetAllOpponents<OpponentViewModel>(userId, ItemsPerPage, (page - 1) * ItemsPerPage);
            var fighters = this.usersService.GetAllFighters<FightersDropDownViewModel>(userId);

            var viewModel = new AllOpponentsViewModel
            {
                Opponents = opponents,
                Fighters = fighters,
            };

            var count = this.fightersService.GetOpponentsCount(userId);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();

            var viewModel = new FighterCreateInputModel
            {
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FighterCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var skillId = await this.skillsService.CreateAsync();
            var biographyId = await this.biographiesService.CreateAsync(input.FirstName, input.Nickname, input.LastName, input.BornCountry, input.Age, input.Picture);
            var fighterId = await this.fightersService.CreateAsync(skillId, biographyId, input.CategoryId, user);
            this.TempData["InfoMessage"] = "Fighter created!";

            return this.RedirectToAction(nameof(this.ChooseOrganization), new { id = fighterId });
        }

        public IActionResult ChooseOrganization()
        {
            var organizationsDropDown = this.organizationsService.GetAll<OrganizationDropDownViewModel>();
            var organizations = this.organizationsService.GetAll<OrganizationViewModel>();

            var viewModel = new ChooseOrganizationViewModel
            {
                Organizations = organizations,
                OrganizationsDropDown = organizationsDropDown,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseOrganization(ChooseOrganizationViewModel viewModel)
        {
            var id = int.Parse(this.RouteData.Values["id"].ToString());

            var user = await this.userManager.GetUserAsync(this.User);

            await this.fightersService.SetOrganization(id, viewModel.OrganizationId, user);

            return this.RedirectToAction("AllFighters", "Users");
        }

        public IActionResult Fight(int fighterId, int opponentId)
        {

            return this.View();
        }
    }
}
