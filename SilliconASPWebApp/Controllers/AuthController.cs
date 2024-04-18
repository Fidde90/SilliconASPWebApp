using Infrastructure.Services;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.ViewModels.Views;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SilliconASPWebApp.Controllers
{
    public class AuthController(UserService userService, AuthService authService, SignInManager<AppUserEntity> signInManager, UserManager<AppUserEntity> userManager, AdminService adminService, ExternalAccountService externalAccountService) : Controller
    {
        private readonly UserService _userService = userService;
        private readonly AuthService _authService = authService;
        private readonly SignInManager<AppUserEntity> _signInManager = signInManager;
        private readonly UserManager<AppUserEntity> _userManager = userManager;
        private readonly AdminService _adminService = adminService;
        private readonly ExternalAccountService _externalAccountService = externalAccountService;

        #region Individual Account

        #region sign up 
        [Route("/signup")]
        public IActionResult SignUp()
        {
            if (_signInManager.IsSignedIn(User))
                RedirectToAction("Details", "Auth");

            return View(new SignUpViewModel());
        }

        [Route("/signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createdUser = await _userService.CreateUserAsync(MappingFactory.UserMapper(viewModel.Form), viewModel.Form.Password);
                    if (createdUser != null)
                        return RedirectToAction("SignIn", "Auth");
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            viewModel.ErrorMessage = "A user with the same email already exsitis";
            return View(viewModel);
        }
        #endregion

        #region sign in 
        [Route("/signin")]
        public IActionResult SignIn(string returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Details", "Account");

            var viewModel = new SignInViewModel();
            viewModel.ReturnUrl = returnUrl ?? "/account";
            return View(viewModel);
        }

        [Route("/signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authService.SignIn(new Models.Forms.SignInFormModel { Email = viewModel.Email, Password = viewModel.Password, Remeber = false });
                    if (result)
                    {
                        var user = await _userService.GetByEmailAsync(viewModel.Email);
                        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

                        if (isAdmin)
                        {
                            var token = await _adminService.GetToken();
                            var cookieOptions = new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                Expires = DateTime.Now.AddDays(1)
                            };

                            Response.Cookies.Append("AccessToken", token, cookieOptions);
                        }

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);

                        return RedirectToAction("Details", "Account");
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            }
            viewModel.ErrorMessage = "Incorrect email or password";
            return View("SignIn", viewModel);
        }
        #endregion

        #endregion

        #region sign out
        public new async Task<IActionResult> SignOut()
        {
            Response.Cookies.Delete("AccessToken");
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region External Account

        [HttpGet]
        public IActionResult Facebook()
        {
            var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback"));
            return new ChallengeResult("Facebook", authProps);
        }

        [HttpGet]
        public async Task<IActionResult> FacebookCallback()
        {
            var facebookData = await _signInManager.GetExternalLoginInfoAsync();
            if (facebookData != null)
            {
                var facebookUser = _externalAccountService.CreateExternalUserObject(facebookData);
                var dbUser = await _userService.GetByEmailAsync(facebookUser);

                dbUser ??= await _externalAccountService.AddExternalUserToDataBaseAsync(facebookUser);

                if (dbUser != null)
                {
                    await _externalAccountService.CompareExternalDataWithDatabase(facebookUser, dbUser);
                    await _signInManager.SignInAsync(dbUser, isPersistent: false);

                    if (HttpContext.User != null)
                        return RedirectToAction("Details", "Account");
                }
            }

            TempData["StatusMessage"] = "Faild to authenticate with Facebook.";
            return RedirectToAction("SignIn", "Auth");
        }

        [HttpGet]
        public IActionResult Google()
        {
            var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleCallback"));
            return new ChallengeResult("Google", authProps);
        }

        [HttpGet]
        public async Task<IActionResult> GoogleCallback()
        {
            var googleData = await _signInManager.GetExternalLoginInfoAsync();
            if (googleData != null)
            {
                var googleUser = _externalAccountService.CreateExternalUserObject(googleData);
                var dbUser = await _userService.GetByEmailAsync(googleUser);

                dbUser ??= await _externalAccountService.AddExternalUserToDataBaseAsync(googleUser);

                if (dbUser != null)
                {
                    await _externalAccountService.CompareExternalDataWithDatabase(googleUser, dbUser);
                    await _signInManager.SignInAsync(dbUser, isPersistent: false);

                    if (HttpContext.User != null)
                        return RedirectToAction("Details", "Account");
                }
            }

            TempData["StatusMessage"] = "Faild to authenticate with Google.";
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion
    }
}
