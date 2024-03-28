using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SilliconASPWebApp.Models.Components;
using System.Text;

namespace SilliconASPWebApp.Controllers
{
    public class CoursesController : Controller
    {
        #region user courses actions
        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7295/api/courses?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IEnumerable<CourseCardModel>>(json);

            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var url = $"https://localhost:7295/api/courses/{id}?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<CourseCardModel>(json);
            data!.GetBackgorundImg();

            return View(data);
        }
        #endregion


        #region admin courses actions
        public IActionResult CreateCourse() => View(new CourseDto());

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseDto newCourse)
        {
            if (newCourse.IsBestSeller == "true" || newCourse.IsBestSeller == "false")
            {
                if (ModelState.IsValid)
                {
                    var url = $"https://localhost:7295/api/courses?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";

                    using var client = new HttpClient();

                    var json = JsonConvert.SerializeObject(newCourse);
                    using var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "created";
                        return RedirectToAction("CreateCourse", "Courses");
                    }
                    TempData["Message"] = "confilct";
                    return View(newCourse);
                }
            }

            TempData["Message"] = "bad data";
            return View(newCourse);
        }


        [HttpGet]
        public async Task<IActionResult> AllCourses(int id)
        {
            var url = $"https://localhost:7295/api/courses?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<CourseCardModel>>(json);

                if (data!.Any())
                    return View(data);

                TempData["Message"] = "no courses";
                return View();

            }
            TempData["Message"] = "faild";
            return View();

        }
        #endregion
    }
}
