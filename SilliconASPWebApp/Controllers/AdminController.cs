﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SilliconASPWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController() : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CoursesOptions()
        {
            return View();
        }
    }
}
