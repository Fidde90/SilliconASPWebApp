using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.Models.Forms;
using SilliconASPWebApp.ViewModels.Views;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Claims;


namespace SilliconASPWebApp.Controllers
{
    [Authorize]
    public class AccountController(SavedCoursesService savedCoursesService, AccountService accountService, UserManager<AppUserEntity> usermanager, AddressService addressService, UserService userService) : Controller
    {
        private readonly UserManager<AppUserEntity> _userManager = usermanager;
        private readonly SavedCoursesService _savedCoursesService = savedCoursesService;
        private readonly AddressService _addressService = addressService;
        private readonly UserService _userService = userService;
        private readonly AccountService _accountService = accountService;


        #region home/details
        [Route("/account")]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            AccountDetailsViewModel viewModel = new();
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
                viewModel.GetUserDetailsData(user!);

            try
            {
                if (user!.AddressId != null)
                {
                    var address = await _addressService.GetOneAddressById((int)user!.AddressId!);
                    viewModel.GetUserAddressData(address!);
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
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
        public IActionResult Security() => View(new SecurityViewModel());

        [HttpPost]
        public async Task<IActionResult> ChangePassword(SecurityFormModel model)
        {
            SecurityViewModel viewModel = new();
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
                return View(nameof(Security), viewModel);

            try
            {
                var changed = await _userManager.ChangePasswordAsync(loggedInUser!, model.Password, model.NewPassword);
                if (changed.Succeeded)
                {
                    viewModel.ChangePasswordErrorMessage = "Password changed!";
                    return View(nameof(Security), viewModel);
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return View(nameof(Security), viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(DeleteAccountFormModel model)
        {
            SecurityViewModel viewModel = new();
            if (!ModelState.IsValid)
                return View(nameof(Security), viewModel);     
            
            try
            {
                var activeUser = await _userManager.GetUserAsync(User);
                if (activeUser != null)
                {
                    var deleteUser = await _userManager.DeleteAsync(activeUser);
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
   
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
                try
                {
                    var courseIds = await _savedCoursesService.GetSavedCoursesIdsAsync(user.Id);
                    var courses = await _savedCoursesService.PostIdsGetCoursesAsync(courseIds);

                    if (courses != null && courses.Count > 0)
                    {
                        viewModel.Courses = courses;
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> SaveCourse(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            try
            {           
                if (user != null)
                    await _savedCoursesService.SaveCourseAsync(id, user.Id);
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return RedirectToAction("Index", "Courses");
        }

        public async Task<IActionResult> ResignCourse(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            try
            {              
                if (user != null)
                    await _savedCoursesService.RemoveCourse(id, user.Id);
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return RedirectToAction("SavedCourses", "Account");
        }

        public async Task<IActionResult> DeleteAll()
        {
            var user = await _userManager.GetUserAsync(User);
            try
            {
                if (user != null)
                    await _savedCoursesService.RemoveAllSavedCourses(user.Id);
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return RedirectToAction("SavedCourses", "Account");
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> UploadImg(IFormFile file)
        {
            try
            {
                var result = await _accountService.UploadProfileImgAsync(User, file);
                if (result == true)
                {
                    TempData["Message"] = "true";
                    return RedirectToAction("Details", "Account");
                }

                TempData["Message"] = "false";
            }
            catch (Exception e) { Debug.WriteLine("Error {0}", e); }
            return RedirectToAction("Details", "Account");
        }
    }
}




