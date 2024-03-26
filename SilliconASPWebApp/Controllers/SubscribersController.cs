using Infrastructure.Dtos;
using Infrastructure.Models.Forms;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SilliconASPWebApp.ViewModels.Views;
using System.Text;
namespace SilliconASPWebApp.Controllers
{
    public class SubscribersController : Controller
    {
        private readonly SubscriberService _subscriberService;

        public SubscribersController()
        {
            _subscriberService = new SubscriberService();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeFormModel model)
        {
            if (ModelState.IsValid)
            {
                var subscriber = new SubscriberDto
                {
                    Email = model.Email
                };

                string url = "https://localhost:7295/api/subscribers";

                using var client = new HttpClient();

                var json = JsonConvert.SerializeObject(subscriber);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["message"] = "subscribed";
                    return Redirect(Url.Action("Index", "Home") + "#subscribe");
                }
            }

            TempData["message"] = "failed";
            return Redirect(Url.Action("Index", "Home") + "#subscribe");
        }

        [HttpGet]
        public async Task<IActionResult> Subscribers()
        {

            TempData["Message"] = "";

            var url = "https://localhost:7295/api/subscribers";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<SubscriberDto>>(json);
                if (data != null && data.Any())
                {
                    return View(data);
                }
            }
            TempData["Message"] = "No Subscribers";
            return View();
        }

        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            var url = "https://localhost:7295/api/subscribers";

            using var client = new HttpClient();
            var response = await client.DeleteAsync($"{url}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("subscribers", "subscribers");
            }

            return RedirectToAction("subscribers", "subscribers");
        }
    }
}
