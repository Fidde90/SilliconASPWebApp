using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.ViewModels.Views;
using System.Diagnostics;

namespace SilliconASPWebApp.Controllers
{
    public class ContactController(ContactService contactService) : Controller
    {
        private readonly ContactService _contactService = contactService;

        public IActionResult Index()
        {
            ContactViewModel Viewmodel = new ContactViewModel();
            return View(Viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(ContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _contactService.SeandMessageAsync(viewModel.Form);

                    if (result == true)
                    {
                        TempData["Message"] = "sent";
                        return RedirectToAction("Index", "Contact");
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            }
            TempData["Message"] = "faild";
            return RedirectToAction("Index", "Contact");
        }
    }
}
