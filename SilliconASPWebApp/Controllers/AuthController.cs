using Infrastructure.Services;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.ViewModels.Views;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SilliconASPWebApp.Controllers
{
    public class AuthController(UserService userService, AuthService authService, SignInManager<AppUserEntity> signInManager, UserManager<AppUserEntity> userManager, IConfiguration configuration) : Controller
    {
        private readonly UserService _userService = userService;
        private readonly AuthService _authService = authService;
        private readonly SignInManager<AppUserEntity> _signInManager = signInManager;
        private readonly UserManager<AppUserEntity> _userManager = userManager;

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
                    var user = await _userService.GetByEmailAsync(viewModel.Email);
                    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

                    if (isAdmin)
                    {
                        string url = "https://localhost:7295/token?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";

                        using var client = new HttpClient();
                        var response = await client.PostAsync(url, null);
                        if (response.IsSuccessStatusCode)
                        {
                            var token = await response.Content.ReadAsStringAsync();
                            var cookieOptions = new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                Expires = DateTime.Now.AddDays(1)
                            };

                            Response.Cookies.Append("AccessToken", token, cookieOptions);
                        }
                    }

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
            var facebookData = await _signInManager.GetExternalLoginInfoAsync(); //hämtar data från facebook
            if (facebookData != null)
            {
                var FbUser = new AppUserEntity
                {
                    FirstName = facebookData.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = facebookData.Principal.FindFirstValue(ClaimTypes.Surname)!,
                    Email = facebookData.Principal.FindFirstValue(ClaimTypes.Email)!,
                    UserName = facebookData.Principal.FindFirstValue(ClaimTypes.Email)!,
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
            var googleData = await _signInManager.GetExternalLoginInfoAsync(); 
            if (googleData != null)
            {
                var FbUser = new AppUserEntity
                {
                    FirstName = googleData.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = googleData.Principal.FindFirstValue(ClaimTypes.Surname)! ?? "-",// att ange ett efternamn var inte ett krav när man skapade ett Google-konto. Men det är ett måste här, så sätter ett värde bara för att där ska finnas något. 
                    Email = googleData.Principal.FindFirstValue(ClaimTypes.Email)!,
                    UserName = googleData.Principal.FindFirstValue(ClaimTypes.Email)!,
                    IsExternal = true
                }; 

                var user = await _userService.GetByEmailAsync(FbUser); 
                if (user == null)
                {
                    var registerNewUser = await _userService.CreateUserNoPasswordAsync(FbUser); 
                    if (registerNewUser) 
                        user = await _userService.GetByEmailAsync(FbUser); 
                }

                if (user != null) 
                {
                    if (user.FirstName != FbUser.FirstName || user.LastName != FbUser.LastName || user.Email != FbUser.Email) 
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

            ModelState.AddModelError("InvalidFacebookAuthentication", "danger|Faild to authenticate with Google.");
            ViewData["StatusMessage"] = "danger|Faild to authenticate with Google.";
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion
    }
}
