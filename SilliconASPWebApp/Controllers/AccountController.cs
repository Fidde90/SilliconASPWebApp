using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.Models.Forms;
using SilliconASPWebApp.Models.Sections;
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
                return RedirectToAction("Details");

            return RedirectToAction("Details");
        }

        [HttpPost]
        public IActionResult AddressInfo(AccountDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Details");

            return RedirectToAction("Details");
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
        }
    }
}
