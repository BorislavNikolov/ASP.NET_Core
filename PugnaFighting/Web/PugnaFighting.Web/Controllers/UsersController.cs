﻿namespace PugnaFighting.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        public IActionResult Info()
        {
            return this.View();
        }
    }
}
