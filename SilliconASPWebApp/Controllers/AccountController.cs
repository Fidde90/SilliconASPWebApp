using Azure;
using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SilliconASPWebApp.Models.Forms;
using SilliconASPWebApp.ViewModels.Views;


namespace SilliconASPWebApp.Controllers
{
    [Authorize] // ser till att du måste vara inloggad för att komma åt dessa sidor.
    public class AccountController(SavedCoursesService savedCoursesService, DataContext dataContext, IConfiguration configuration, HttpClient httpClient, AccountService accountService, UserManager<AppUserEntity> usermanager, AddressService addressService, UserService userService) : Controller
    {

        private readonly DataContext _dataContext = dataContext;

        private readonly UserManager<AppUserEntity> _userManager = usermanager;
        private readonly SavedCoursesService _savedCoursesService = savedCoursesService;
        private readonly AddressService _addressService = addressService;
        private readonly UserService _userService = userService;
        private readonly AccountService _accountService = accountService;
        private readonly HttpClient _client = httpClient;
        private readonly IConfiguration _configuration = configuration;

        #region home/details
        [Route("/account")]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            AccountDetailsViewModel viewModel = new();

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
                viewModel.GetUserDetailsData(user!);

            if (user!.AddressId != null)
            {
                var address = await _addressService.GetOneAddressById((int)user!.AddressId!);
                viewModel.GetUserAddressData(address!);
            }

            return View(nameof(Details), viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BasicInfo(AccountBasicInfoFormModel model)
        {
            AccountDetailsViewModel viewModel = new();

            if (!ModelState.IsValid)
                return View(nameof(Details), viewModel);

            var user = await _userManager.GetUserAsync(User);
            var loggedInUser = MappingFactory.MapNewUserValues(user!, model);

            if (loggedInUser != null)
            {
                var updateUser = await _userManager.UpdateAsync(loggedInUser);
                viewModel.GetUserDetailsData(loggedInUser!);

                if (user!.AddressId != null)
                {
                    var address = await _addressService.GetOneAddressById((int)user!.AddressId!);
                    viewModel.GetUserAddressData(address!);
                }
            }

            return View(nameof(Details), viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddressInfo(AccountAddressFormModel model)
        {
            AccountDetailsViewModel viewModel = new();

            if (!ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                viewModel.GetUserDetailsData(user!);
                viewModel.GetUserAddressData(user!.Address!);
                return View(nameof(Details), viewModel);
            }

            var loggedInUser = await _userManager.GetUserAsync(User);

            var newAddress = MappingFactory.NewAddressMapping(model);
            var updatedAddress = await _addressService.UpdateAddress(newAddress);

            if (updatedAddress != null)
            {
                loggedInUser!.AddressId = updatedAddress.Id;
                var updateUser = await _userService.UpdateUser(loggedInUser);
                viewModel.GetUserAddressData(loggedInUser.Address!);
                viewModel.GetUserDetailsData(updateUser!);
            }

            return View(nameof(Details), viewModel);
        }
        #endregion

        #region security
        [HttpGet]
        public IActionResult Security()
        {
            SecurityViewModel viewModel = new();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(SecurityFormModel model)
        {
            SecurityViewModel viewModel = new();

            if (!ModelState.IsValid)
                return View(nameof(Security), viewModel);

            var loggedInUser = await _userManager.GetUserAsync(User);

            if (loggedInUser != null)
            {
                var updateUser = await _userManager.ChangePasswordAsync(loggedInUser!, model.Password, model.NewPassword);
            }

            viewModel.ChangePasswordErrorMessage = "Passwords did not match.";
            return View(nameof(Security), viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(DeleteAccountFormModel model)
        {
            SecurityViewModel viewModel = new();

            if (!ModelState.IsValid)
                return View(nameof(Security), viewModel);

            var activeUser = await _userManager.GetUserAsync(User);

            if (activeUser != null)
            {
                var deleteUser = await _userManager.DeleteAsync(activeUser);
            }

            viewModel.DeleteAccountErrorMessage = "Confirm the checkbox.";
            return RedirectToAction("Account", "Details");
        }
        #endregion

        #region saved courses

        [Route("/savedcourses")]
        public async Task<IActionResult> SavedCourses(SavedCoursesViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var courseIds = await _savedCoursesService.GetSavedCoursesIdsAsync(user.Id);
                var courses = await _savedCoursesService.PostIdsGetCoursesAsync(courseIds);

                if(courses != null && courses.Count > 0)
                {
                    viewModel.Courses = courses;         
                }
                return View(viewModel);
            }
            return View(viewModel);
        }
        public async Task<IActionResult> SaveCourse(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var newSave = new SavedCoursesEntity
                {
                    CourseId = id,
                    UserId = user.Id
                };

                var result = await _savedCoursesService.SaveCourseAsync(newSave);

                if (result == true)
                {

                    return RedirectToAction("Index", "Courses");
                }
            }

            return RedirectToAction("Index", "Courses");
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> UploadImg(IFormFile file)
        {
            var result = await _accountService.UploadProfileImgAsync(User, file);
            if (result == true)
            {
                TempData["Message"] = "true";
                return RedirectToAction("Details", "Account");
            }

            TempData["Message"] = "false";
            return RedirectToAction("Details", "Account");
        }
    }
}




