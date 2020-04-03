namespace PugnaFighting.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ManagersController : Controller
    {
        [Authorize]
        public IActionResult All()
        {
            return this.View();
        }
    }
}
