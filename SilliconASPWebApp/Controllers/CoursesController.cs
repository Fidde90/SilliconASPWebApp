using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SilliconASPWebApp.Models.Components;
using SilliconASPWebApp.ViewModels.Views;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace SilliconASPWebApp.Controllers
{
    public class CoursesController(IConfiguration configuration, CourseService courseService, CategoryService categoryService, HttpClient httpClient) : Controller
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly string _url = "https://localhost:7295/api/courses"; //släng in i appsettings sen
        private readonly CategoryService _categoryService = categoryService;
        private readonly HttpClient _client = httpClient;
        private readonly CourseService _courseService = courseService;

        #region user courses actions

        [HttpGet]
        public async Task<IActionResult> Index(string category = "", string searchValue = "", int pageNumber = 1, int pageSize = 2)
        {
            var courseResult = await _courseService.GetCoursesAsync(category, searchValue, pageNumber, pageSize);

            if(courseResult != null)
            {
                var viewModel = new CoursesIndexViewModel
                {
                    Categories = await _categoryService.GetCategoriesAsync(),
                    Courses = courseResult.Courses,
                    Pagination = new PaginationDto
                    {
                        PageSize = pageSize,
                        CurrentPage = pageNumber,
                        TotalPages = courseResult.TotalPages,
                        TotalItems = courseResult.TotalItems,
                        Category = courseResult.Category
                    }
                };

                return View(viewModel);
            }
                return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var url = $"https://localhost:7295/api/courses/{id}?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";

            var response = await _client.GetAsync(url);
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
            if (ModelState.IsValid)
            {
                if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var url = $"https://localhost:7295/api/courses?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";
                    var json = JsonConvert.SerializeObject(newCourse);
                    using var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync(url, content);
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
        public async Task<IActionResult> AllCourses()
        {
            try
            {
                var result = await _courseService.GetCoursesAsync();
                if (result != null)
                    return View(result.Courses);

                TempData["Message"] = "no courses";
                return View();
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            TempData["Message"] = "faild";
            return View();
        }

        public async Task<IActionResult> UpdateCourse(int id)
        {
            var url = $"https://localhost:7295/api/courses/{id}?key={_configuration["ApiKey:Secret"]}";

            var response = await _client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<CourseCardModel>(json);
            data!.GetBackgorundImg();

            if (data != null)
                return View(data);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(CourseCardModel dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _courseService.UpdateCourseAsync(dto);
                    if (result != null)
                    {
                        TempData["message"] = "Updated";
                        return RedirectToAction("UpdateCourse", "Courses");
                    }

                    return RedirectToAction("UpdateCourse", "Courses");
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }

            return RedirectToAction("UpdateCourse", "Courses");
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                var url = "https://localhost:7295/api/courses";

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.DeleteAsync($"{url}/{id}?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("AllCourses", "Courses");
                }
            }
            return RedirectToAction("UpdateCourse", "Courses");
        }
        #endregion
    }
}
