using Infrastructure.Dtos;
using Infrastructure.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
namespace SilliconASPWebApp.Controllers
{
    public class SubscribersController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var subscriber = new SubscriberDto
                    {
                        Email = model.Email
                    };
                    string url = "https://localhost:7295/api/subscribers?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";

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
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }         
            }
            TempData["message"] = "failed";
            return Redirect(Url.Action("Index", "Home") + "#subscribe");
        }

        [HttpGet]
        public async Task<IActionResult> Subscribers()
        {
            TempData["Message"] = "";
            try
            {
                var url = "https://localhost:7295/api/subscribers?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";
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
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }       
            TempData["Message"] = "No Subscribers";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SubscriberDetails(int id)
        {
            var url = "https://localhost:7295/api/subscribers";
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync($"{url}/{id}?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<SubscriberDto>(json);
                    var formData = new UpdateSubscriberFormModel
                    {
                        Id = data!.Id,
                        Email = data!.Email
                    };
                    return View(formData);
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return RedirectToAction("subscribers", "subscribers");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubscriber(UpdateSubscriberFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string newEmail = model.Email;
                    int id = model.Id;

                    string url = "https://localhost:7295/api/subscribers";

                    using var client = new HttpClient();
                    using var content = new StringContent("", Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"{url}/{id}?newEmail={newEmail}&key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1", content);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["message"] = "Updated";
                        return RedirectToAction("SubscriberDetails", new { id });
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            }
            TempData["message"] = "failed";
            return RedirectToAction("SubscriberDetails", new { model.Id });
        }

        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            var url = "https://localhost:7295/api/subscribers";

            try
            {
                using var client = new HttpClient();
                var response = await client.DeleteAsync($"{url}/{id}?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("subscribers", "subscribers");
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return RedirectToAction("subscribers", "subscribers");
        }
    }
}
