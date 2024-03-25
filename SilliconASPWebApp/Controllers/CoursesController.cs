using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SilliconASPWebApp.Models.Components;

namespace SilliconASPWebApp.Controllers
{
    public class CoursesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7295/api/courses";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IEnumerable<CourseCardModel>>(json);
  
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var url = $"https://localhost:7295/api/courses/{id}";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<CourseCardModel>(json);
            data!.GetBackgorundImg();

            return View(data);
        }
    }
}
