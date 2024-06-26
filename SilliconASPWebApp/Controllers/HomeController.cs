﻿using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.ViewModels.Views;

namespace SilliconASPWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {         
            HomeIndexViewModel viewModel = new HomeIndexViewModel();     

            ViewData["Title"] = viewModel.Title = "Home";
            return View(viewModel);
        }

        [Route("/error")]
        public IActionResult Error404(int statuscode)
        {
            return View();
        }

        [Route("/denied")]
        public IActionResult Denied(int statuscode)
        {
            return View();
        }
    }
}
