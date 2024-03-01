using Microsoft.AspNetCore.Mvc;
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
            viewModel.ErrorMessage = "Incorrect email or password ";
            return View(nameof(Details), viewModel);

        }

        [HttpPost]
        public IActionResult AddressInfo(AccountDetailsViewModel viewModel)
        {
            viewModel.ErrorMessage = "Incorrect email or password ";
            return View(nameof(Details), viewModel);
        }
    }
}
