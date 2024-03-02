﻿using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.ViewModels.Views;

namespace SilliconASPWebApp.Controllers
{
    public class AccountController : Controller
    {
        [Route("/account")]
        [HttpGet]
        public IActionResult Details()
        {
            AccountDetailsViewModel viewModel = new AccountDetailsViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult BasicInfo(AccountDetailsViewModel viewModel)
        {

            if (!ModelState.IsValid)
                return View(nameof(Details), viewModel);


            viewModel.ErrorMessage = "Incorrect email or password ";
            return View(nameof(Details), viewModel);

        }

        [HttpPost]
        public IActionResult AddressInfo(AccountDetailsViewModel viewModel)
        {

            if (!ModelState.IsValid)
                return View(nameof(Details), viewModel);

      
        }

        [HttpGet]
        public IActionResult Security()
        {
            SecurityViewModel viewModel = new SecurityViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ChangePassword(SecurityViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(Security), viewModel);

            viewModel.ChangePasswordErrorMessage = "Passwords did not match.";
            return RedirectToAction("Account", "Details");
        }

        [HttpPost]
        public IActionResult DeleteAccount(SecurityViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(Security), viewModel);

            viewModel.DeleteAccountErrorMessage = "Confirm the checkbox.";
            return RedirectToAction("Account", "Details");
        }

        [Route("/courses")]
        [HttpGet]
        public IActionResult Courses()
        {
            SavedCoursesViewModel viewModel = new SavedCoursesViewModel();
            return View(viewModel);

            viewModel.ErrorMessage = "Incorrect email or password ";
            return View(nameof(Details), viewModel);

        }
    }
}
