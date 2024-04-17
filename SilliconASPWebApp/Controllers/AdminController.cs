using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SilliconASPWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController() : Controller
    {
        public IActionResult Index() => View();

        public IActionResult CoursesOptions() => View();
    }
}
