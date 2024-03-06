using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using SilliconASPWebApp.Models.Forms;

namespace Infrastructure.Services
{
    public class AuthService
    {
        private readonly SignInManager<AppUserEntity> _signInManager;

        public AuthService(SignInManager<AppUserEntity> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> SignIn(SignInFormModel model)
        {
            try
            {
                var signedIn = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remeber, false);
                if (signedIn.Succeeded)
                    return true;
            }
            catch (Exception e) { }
            return false;
        }
    }
}
