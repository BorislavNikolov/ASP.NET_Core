namespace PugnaFighting.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        public IActionResult Info()
        {
            return this.View();
        }
    }
}
