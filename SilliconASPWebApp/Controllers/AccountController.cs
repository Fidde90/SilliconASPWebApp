using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.Models.Forms;
using SilliconASPWebApp.ViewModels.Views;
using System.Net;

namespace SilliconASPWebApp.Controllers
{
    //[Authorize] // ser till att du måste vara inloggad för att komma åt dessa sidor.
    public class AccountController : Controller
    {
        private readonly UserManager<AppUserEntity> _usermanager;
        private readonly AddressService _addressService;
        private readonly UserService _userService;

        public AccountController(UserManager<AppUserEntity> usermanager, AddressService addressService, UserService userService)
        {
            _usermanager = usermanager;
            _addressService = addressService;
            _userService = userService;
        }

        #region home/details
        [Route("/account")]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            AccountDetailsViewModel viewModel = new();

            var user = await _usermanager.GetUserAsync(User);
            var address = await _addressService.GetOneAddressById((int)user!.AddressId!);

            viewModel.GetUserDetailsData(user!);
            viewModel.GetUserAddressData(address!);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BasicInfo(AccountBasicInfoFormModel Model)
        {
            AccountDetailsViewModel viewModel = new();

            if (!ModelState.IsValid)
                return View(nameof(Details), viewModel);

            var loggedInUser = await _usermanager.GetUserAsync(User);

            loggedInUser!.FirstName = Model.FirstName;
            loggedInUser.LastName = Model.LastName;
            loggedInUser.Email = Model.Email;
            loggedInUser.PhoneNumber = Model.Phone;
            loggedInUser.Bio = Model.Bio;
            loggedInUser.Email = Model.Email;

            var updateUser = await _usermanager.UpdateAsync(loggedInUser);

            viewModel.GetUserAddressData(loggedInUser!.Address!);
            viewModel.GetUserDetailsData(loggedInUser!);

            return View(nameof(Details), viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddressInfo(AccountAddressFormModel model)
        {
            AccountDetailsViewModel viewModel = new();

            if (!ModelState.IsValid)
                return View(nameof(Details), viewModel);

            var loggedInUser = await _usermanager.GetUserAsync(User);

            var newAddress = new AddressEntity
            {
                AddressLine_1 = model.Addressline_1,
                AddressLine_2 = model.Addressline_2,
                City = model.City,
                PostalCode = model.PostalCode
            };


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
        #endregion

        #region change password
        [HttpPost]
        public async Task<IActionResult> ChangePassword(SecurityFormModel model)
        {
            SecurityViewModel viewModel = new();

            if (!ModelState.IsValid)
                return View(nameof(Security), viewModel);

            var loggedInUser = await _usermanager.GetUserAsync(User);
            var updateUser = await _usermanager.ChangePasswordAsync(loggedInUser,model.Password,model.NewPassword);
            
         
       




            viewModel.ChangePasswordErrorMessage = "Passwords did not match.";
            return View(nameof(Security), viewModel);
        }
        #endregion

        #region delete
        [HttpPost]
        public IActionResult DeleteAccount(SecurityViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(Security), viewModel);

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
