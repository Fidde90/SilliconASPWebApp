using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.Models.Forms;
using SilliconASPWebApp.ViewModels.Views;
using System.Net;

namespace SilliconASPWebApp.Controllers
{
    [Authorize] // ser till att du måste vara inloggad för att komma åt dessa sidor.
    public class AccountController(UserManager<AppUserEntity> usermanager, AddressService addressService, UserService userService) : Controller
    {
        private readonly UserManager<AppUserEntity> _usermanager = usermanager;
        private readonly AddressService _addressService = addressService;
        private readonly UserService _userService = userService;

        #region home/details
        [Route("/account")]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            AccountDetailsViewModel viewModel = new();

            var user = await _usermanager.GetUserAsync(User);

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

            var user = await _usermanager.GetUserAsync(User);
            var loggedInUser = MappingFactory.MapNewUserValues(user!, model);

            if (loggedInUser != null)
            {
                var updateUser = await _usermanager.UpdateAsync(loggedInUser);
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
                var user = await _usermanager.GetUserAsync(User);
                viewModel.GetUserDetailsData(user!);
                viewModel.GetUserAddressData(user!.Address!);
                return View(nameof(Details), viewModel);
            }
    
            var loggedInUser = await _usermanager.GetUserAsync(User);

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

            var loggedInUser = await _usermanager.GetUserAsync(User);

            if (loggedInUser != null)
            {
                var updateUser = await _usermanager.ChangePasswordAsync(loggedInUser!, model.Password, model.NewPassword);
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

            var activeUser = await _usermanager.GetUserAsync(User);

            if (activeUser != null)
            {
                var deleteUser = await _usermanager.DeleteAsync(activeUser);
            }

            viewModel.DeleteAccountErrorMessage = "Confirm the checkbox.";
            return RedirectToAction("Account", "Details");
        }
        #endregion

        #region saved courses
        [Route("/courses")]
        [HttpGet]
        public IActionResult Courses(SavedCoursesViewModel viewModel)
        {
            return View(viewModel);
        }
        #endregion
    }
}




