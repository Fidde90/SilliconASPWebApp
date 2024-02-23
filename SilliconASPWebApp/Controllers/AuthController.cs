using Microsoft.AspNetCore.Mvc;

namespace SilliconASPWebApp.Controllers
{
    public class AuthController : Controller
    {
        [Route("/signup")]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
