using Infrastructure.Services;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc;
using SilliconASPWebApp.ViewModels.Views;
using Microsoft.AspNetCore.Authentication;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace SilliconASPWebApp.Controllers
{
    public class AuthController(UserService userService, AuthService authService, SignInManager<AppUserEntity> signInManager) : Controller
    {
        private readonly UserService _userService = userService;
        private readonly AuthService _authService = authService;
        private readonly SignInManager<AppUserEntity> _signInManager = signInManager;

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
                var created = await _userService.CreateUser(userFactory.UserMapper(viewModel.Form), viewModel.Form.Password);
                if (created)
                    return RedirectToAction("SignIn", "Auth");
            }
            viewModel.ErrorMessage = "A user with the same email already exsitis";
            return View(viewModel);
        }
        #endregion

        #region sign in 
        [Route("/signin")]
        public IActionResult SignIn()
        {
            if (_signInManager.IsSignedIn(User))
                RedirectToAction("Details", "Auth");

            return View(new SignInViewModel());  
        }


        [Route("/signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.SignIn(viewModel.Form);
                if (result)
                    return RedirectToAction("Details", "Account");
            }

            viewModel.ErrorMessage = "Incorrect email or password";
            return View(viewModel);
        }
        #endregion

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Auth");
        }
    }
}
