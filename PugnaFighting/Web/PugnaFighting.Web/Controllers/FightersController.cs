using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PugnaFighting.Web.Controllers
{
    public class FightersController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
