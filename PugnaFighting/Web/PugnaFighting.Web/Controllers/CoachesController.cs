namespace PugnaFighting.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Coaches;

    [Authorize]
    public class CoachesController : Controller
    {
        private readonly ICoachesService coachesService;

        public CoachesController(ICoachesService coachesService)
        {
            this.coachesService = coachesService;
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
            var coachViewModel = this.coachesService.GetById<DetailsCoachViewModel>(id);

            if (coachViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(coachViewModel);
        }
    }
}
