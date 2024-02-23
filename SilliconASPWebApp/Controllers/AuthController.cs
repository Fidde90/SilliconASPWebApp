using Microsoft.AspNetCore.Mvc;

namespace SilliconASPWebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
