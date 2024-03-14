using Infrastructure.Services;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.ViewModels.Views;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SilliconASPWebApp.Controllers
{
    public class AuthController(UserService userService, AuthService authService, SignInManager<AppUserEntity> signInManager) : Controller
    {
        private readonly UserService _userService = userService;
        private readonly AuthService _authService = authService;
        private readonly SignInManager<AppUserEntity> _signInManager = signInManager;

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
            if (ModelState.IsValid)
            {
                var createdUser = await _userService.CreateUserAsync(MappingFactory.UserMapper(viewModel.Form), viewModel.Form.Password);
                if (createdUser != null)
                    return RedirectToAction("SignIn", "Auth");
            }
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
                var result = await _authService.SignIn(new Models.Forms.SignInFormModel { Email = viewModel.Email, Password = viewModel.Password, Remeber = false });
                if (result)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Details", "Account");
                }
            }
            viewModel.ErrorMessage = "Incorrect email or password";
            return View("SignIn", viewModel);
        }
        #endregion

        #endregion

        #region sign out
        public new async Task<IActionResult> SignOut()
        {
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
            var data = await _signInManager.GetExternalLoginInfoAsync(); //hämtar data från facebook
            if (data != null)
            {
                var FbUser = new AppUserEntity
                {
                    FirstName = data.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = data.Principal.FindFirstValue(ClaimTypes.Surname)!,
                    Email = data.Principal.FindFirstValue(ClaimTypes.Email)!,
                    UserName = data.Principal.FindFirstValue(ClaimTypes.Email)!,
                    IsExternal = true
                }; // skapar ny användare med nya datan

                var user = await _userService.GetByEmailAsync(FbUser); //hämtar upp användaren via email i databasen (om den finns)
                if (user == null)//om inte
                {
                    var registerNewUser = await _userService.CreateUserNoPasswordAsync(FbUser); // skapar den
                    if (registerNewUser) // om den lyckades
                        user = await _userService.GetByEmailAsync(FbUser); // nu sparar vi in den skapade/hämtade användaren i "user variabeln"
                }

                if (user != null) //antingen fanns användaren redan eller så har vi nu skapat den vid dehär laget. Så nu ska den inte vara null
                {
                    if (user.FirstName != FbUser.FirstName || user.LastName != FbUser.LastName || user.Email != FbUser.Email)// om det finns skillnader i datan 
                    {
                        user.FirstName = FbUser.FirstName;
                        user.LastName = FbUser.LastName;
                        user.Email = FbUser.Email;

                        await _userService.UpdateWithUserManagerAsync(user);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (HttpContext.User != null)
                        return RedirectToAction("Details", "Account");
                }
            }

            ModelState.AddModelError("InvalidFacebookAuthentication", "danger|Faild to authenticate with Facebook.");
            ViewData["StatusMessage"] = "danger|Faild to authenticate with Facebook.";
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
            var data = await _signInManager.GetExternalLoginInfoAsync(); //hämtar data från facebook
            if (data != null)
            {
                var FbUser = new AppUserEntity
                {
                    FirstName = data.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = data.Principal.FindFirstValue(ClaimTypes.Surname)! ?? "",
                    Email = data.Principal.FindFirstValue(ClaimTypes.Email)!,
                    UserName = data.Principal.FindFirstValue(ClaimTypes.Email)!,
                    IsExternal = true
                }; // skapar ny användare med nya datan

                var user = await _userService.GetByEmailAsync(FbUser); //hämtar upp användaren via email i databasen (om den finns)
                if (user == null)//om inte
                {
                    var registerNewUser = await _userService.CreateUserNoPasswordAsync(FbUser); // skapar den
                    if (registerNewUser) // om den lyckades
                        user = await _userService.GetByEmailAsync(FbUser); // nu sparar vi in den skapade/hämtade användaren i "user variabeln"
                }

                if (user != null) //antingen fanns användaren redan eller så har vi nu skapat den vid dehär laget. Så nu ska den inte vara null
                {
                    if (user.FirstName != FbUser.FirstName || user.LastName != FbUser.LastName || user.Email != FbUser.Email)// om det finns skillnader i datan 
                    {
                        user.FirstName = FbUser.FirstName;
                        user.LastName = FbUser.LastName;
                        user.Email = FbUser.Email;

                        await _userService.UpdateWithUserManagerAsync(user);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (HttpContext.User != null)
                        return RedirectToAction("Details", "Account");
                }
            }

            ModelState.AddModelError("InvalidFacebookAuthentication", "danger|Faild to authenticate with Facebook.");
            ViewData["StatusMessage"] = "danger|Faild to authenticate with Facebook.";
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion
    }
}
