using Microsoft.AspNetCore.Mvc;

namespace SilliconASPWebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
