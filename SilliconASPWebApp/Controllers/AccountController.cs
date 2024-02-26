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
            return View(nameof(Details), viewModel);
        }

        [HttpPost]
        public IActionResult AddressInfo(AccountDetailsViewModel viewModel)
        {
            return View(nameof(Details), viewModel);
        }
    }
}
