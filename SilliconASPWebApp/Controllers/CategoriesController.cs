using Infrastructure.Factories;
using Infrastructure.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace SilliconASPWebApp.Controllers
{
    public class CategoriesController(IConfiguration configuration) : Controller
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly string _url = "https://localhost:7295/api/Category";

        [Route("/Category")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = CategoryMapper.ToCreateCategoryDto(model);
                    using var client = new HttpClient();

                    var json = JsonConvert.SerializeObject(category);
                    using var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{_url}?key={_configuration["ApiKey:Secret"]}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "created";
                        return RedirectToAction("Index", "Categories");
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e}"); }
   
                TempData["Message"] = "confilct";
            }

            return RedirectToAction("Index", "Categories");
        }
    }
}
