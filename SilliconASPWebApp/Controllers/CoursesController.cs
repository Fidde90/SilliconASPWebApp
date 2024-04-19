using Infrastructure.Dtos;
using Infrastructure.Factories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.ViewModels.Views;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace SilliconASPWebApp.Controllers
{
    [Authorize]
    public class CoursesController(CourseService courseService, CategoryService categoryService) : Controller
    {
        private readonly CategoryService _categoryService = categoryService;
        private readonly CourseService _courseService = courseService;

        #region user courses actions
        public async Task<IActionResult> Index(string category = "", string searchValue = "", int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var courseResult = await _courseService.GetCoursesAsync(category, searchValue, pageNumber, pageSize);
                if (courseResult != null)
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
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            TempData["ErrorMessage"] = "error";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var course = await _courseService.GetOneCourseAsync(id);
                if (course != null)
                    return View(course);
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            TempData["ErrorMessage"] = "error";
            return View();
        }
        #endregion


        #region admin courses actions
        public IActionResult CreateCourse() => View(new CourseDto());

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseDto newCourse)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
                    {
                        var IsCreated = await _courseService.CreateCourseAsync(newCourse, token);
                        if (IsCreated == true)
                        {
                            TempData["Message"] = "created";
                            return RedirectToAction("CreateCourse", "Courses");
                        }
                        TempData["Message"] = "confilct";
                        return View(newCourse);
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
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
                    return View(result);

                TempData["Message"] = "no courses";
                return View();
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            TempData["Message"] = "faild";
            return View();
        }

        public async Task<IActionResult> ManageCourse(int id)
        {
            if (id >= 0)
            {
                try
                {
                    var course = await _courseService.GetOneCourseAsync(id);
                    var updateCourse = CourseMapping.ToUpdateCourseDto(course);
                    if (course != null)
                    {
                        return View(updateCourse);
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto dto)
        {
            TempData["message"] = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
                    {
                        var result = await _courseService.UpdateCourseAsync(dto, token);

                        if (result != null)
                        {
                            TempData["message"] = "Updated";
                            return RedirectToAction("ManageCourse", "Courses", result);
                        }
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            TempData["message"] = "bad data";
            return RedirectToAction("ManageCourse", "Courses", dto);
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
                {
                    var result = await _courseService.DeleteCourseAsync(id,token);
                    if (result == true)
                    {
                        return RedirectToAction("AllCourses", "Courses");
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return RedirectToAction("AllCourses", "Courses");
        }
        #endregion
    }
}
