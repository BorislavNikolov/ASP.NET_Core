﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pugna.Controllers
{
    public class ManagersController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
