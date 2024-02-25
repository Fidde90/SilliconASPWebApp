using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.ViewModels.Views;

namespace SilliconASPWebApp.Controllers
{
    public class AccountController : Controller
    {
        [Route("/")]
        [HttpGet]
        public IActionResult Details()
        {
            AccountDetailsViewModel viewModel = new AccountDetailsViewModel();
            return View(viewModel);
        }
    }
}
