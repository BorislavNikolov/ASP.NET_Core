namespace PugnaFighting.Web.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Data;
    using PugnaFighting.Web.ViewModels.Fighters;

    [Authorize]
    public class FightersController : Controller
    {
        private readonly IFightersService fightersService;
        private readonly ICategoriesService categoriesService;
        private readonly IBiographiesService biographiesService;
        private readonly ISkillsService skillsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public FightersController(
            IFightersService fightersService,
            ICategoriesService categoriesService,
            IBiographiesService biographiesService,
            ISkillsService skillsService,
            UserManager<ApplicationUser> userManager)
        {
            this.fightersService = fightersService;
            this.categoriesService = categoriesService;
            this.biographiesService = biographiesService;
            this.skillsService = skillsService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            return this.View();
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
        [Authorize]
        public async Task<IActionResult> Create(FighterCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var skillId = await this.skillsService.CreateAsync();
            var biographyId = await this.biographiesService.CreateAsync(input.FirstName, input.Nickname, input.LastName, input.BornCountry, input.Age);
            var fighterId = await this.fightersService.CreateAsync(skillId, biographyId, input.CategoryId, user.Id);
            this.TempData["InfoMessage"] = "Fighter created!";

            return this.RedirectToAction(nameof(this.ChooseOrganization), new { id = fighterId });
        }

        public IActionResult ChooseOrganization()
        {
            return this.View();
        }

        public IActionResult Fight()
        {
            return this.View();
        }
    }
}
