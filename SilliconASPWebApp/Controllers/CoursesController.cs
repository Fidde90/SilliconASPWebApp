﻿using Microsoft.AspNetCore.Mvc;

namespace SilliconASPWebApp.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
