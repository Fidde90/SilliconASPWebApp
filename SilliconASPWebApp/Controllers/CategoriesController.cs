using Infrastructure.Models.Forms;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SilliconASPWebApp.Controllers
{
    public class CategoriesController(CategoryService categoryService) : Controller
    {
        private readonly CategoryService _categoryService = categoryService;

        [Route("/Category")]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryFormModel model)
        {
            if (ModelState.IsValid && model.CategoryName.Length > 1)
            {
                try
                {
                    var result = await _categoryService.CreateCategoryAsync(model);

                    if (result == "created")
                    {
                        TempData["Message"] = result;
                        return RedirectToAction("Index", "Categories");
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e}"); }

                TempData["Message"] = "confilct";
                return RedirectToAction("Index", "Categories");
            }
            TempData["Message"] = "bad data";
            return RedirectToAction("Index", "Categories");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (categoryId >= 0)
            {
                try
                {
                    var result = await _categoryService.DeleteCategoryAsync(categoryId);

                    if (result == true)
                    {
                        TempData["Message"] = "deleted";
                        return RedirectToAction("Index", "Categories");
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e}"); }
            }
            TempData["Message"] = "error";
            return RedirectToAction("Index", "Categories");
        }
    }
}
