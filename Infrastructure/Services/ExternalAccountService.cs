using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class ExternalAccountService
    {
        private readonly SignInManager<AppUserEntity> _signInManager;
        private readonly UserService _userService;

        public ExternalAccountService(SignInManager<AppUserEntity> signInManager, UserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }

        public AppUserEntity CreateExternalUserObject(ExternalLoginInfo externalData)
        {
            if (externalData != null)
            {
                var externalUser = new AppUserEntity
                {
                    FirstName = externalData.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = externalData.Principal.FindFirstValue(ClaimTypes.Surname)!,
                    Email = externalData.Principal.FindFirstValue(ClaimTypes.Email)!,
                    UserName = externalData.Principal.FindFirstValue(ClaimTypes.Email)!,
                    IsExternal = true
                };

                return externalUser;
            }
            return null!;
        }

        public async Task<AppUserEntity> AddExternalUserToDataBaseAsync(AppUserEntity externalUser)
        {
            var registerNewUser = await _userService.CreateUserNoPasswordAsync(externalUser);
            if (registerNewUser == true)
            {
                var dbUser = await _userService.GetByEmailAsync(externalUser);
                return dbUser;
            }
            return null!;
        }

        public async Task<AppUserEntity> CompareExternalDataWithDatabase(AppUserEntity externalUser, AppUserEntity databseUser)
        {
            if (databseUser.FirstName != externalUser.FirstName || databseUser.LastName != externalUser.LastName || databseUser.Email != externalUser.Email)
            {
                databseUser.FirstName = externalUser.FirstName;
                databseUser.LastName = externalUser.LastName;
                databseUser.Email = externalUser.Email;

                var updateUser = await _userService.UpdateWithUserManagerAsync(databseUser);

                if (updateUser == true)
                {
                    return databseUser;             
                }
            }
            return null!;
        }
    }
}
