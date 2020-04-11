namespace PugnaFighting.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PugnaFighting.Services.Data.Contracts;
    using PugnaFighting.Web.ViewModels.Managers;

    [Authorize]
    public class ManagersController : Controller
    {
        private readonly IManagersService managersService;

        public ManagersController(IManagersService managersService)
        {
            this.managersService = managersService;
        }

        public IActionResult All()
        {
            var managers = this.managersService.GetAll<ManagerViewModel>();

            var viewModel = new AllManagersViewModel
            {
                ManagerViewModels = managers,
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var managerViewModel = this.managersService.GetById<DetailsManagerViewModel>(id);

            if (managerViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(managerViewModel);
        }
    }
}
