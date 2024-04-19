using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class ExternalAccountService(UserService userService)
    {
        private readonly UserService _userService = userService;

        public AppUserEntity CreateExternalUserObject(ExternalLoginInfo externalData)
        {
            try
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
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<AppUserEntity> AddExternalUserToDataBaseAsync(AppUserEntity externalUser)
        {
            try
            {
                var registerNewUser = await _userService.CreateUserNoPasswordAsync(externalUser);
                if (registerNewUser == true)
                {
                    var dbUser = await _userService.GetByEmailAsync(externalUser);
                    return dbUser;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<AppUserEntity> CompareExternalDataWithDatabase(AppUserEntity externalUser, AppUserEntity databseUser)
        {
            try
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
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
