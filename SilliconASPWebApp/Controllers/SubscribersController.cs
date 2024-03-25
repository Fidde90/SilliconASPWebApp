using Infrastructure.Dtos;
using Infrastructure.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SilliconASPWebApp.ViewModels.Views;
using System.Text;

namespace SilliconASPWebApp.Controllers
{
    public class SubscribersController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeFormModel model)
        {
            HomeIndexViewModel viewModel = new HomeIndexViewModel();

            if (ModelState.IsValid)
            {
                var subscriber = new SubscriberDto
                {
                    Email = model.Email
                };

                string Url = "https://localhost:7295/api/subscribers";

                using var client = new HttpClient();

                var json = JsonConvert.SerializeObject(subscriber);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Url,content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["message"] = "subscribed";
                    return Redirect("~/Home/Index#subscribe");
                }
            }

            TempData["message"] = "failed";
            return Redirect("~/Home/Index#subscribe");
        }
    }
}
