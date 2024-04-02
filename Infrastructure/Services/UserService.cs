using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class UserService
    {
        private readonly UserManager<AppUserEntity> _userManager;
        private readonly UserRepository _UserRepository;

        public UserService(UserManager<AppUserEntity> userManager, UserRepository UserRepository)
        {
            _userManager = userManager;
            _UserRepository = UserRepository;
        }

        public async Task<AppUserEntity> CreateUserAsync(AppUserEntity newUser, string password)
        {
            try
            {
                if (!await _UserRepository.Exists(x => x.Email == newUser.Email))
                {
                    var result = await _userManager.CreateAsync(newUser, password);
                    if (result.Succeeded)
                    {
                        var numbersOfUsers = _userManager.Users.Count();
                        if (numbersOfUsers > 1)
                            await _userManager.AddToRoleAsync(newUser, "User");
                        else
                            await _userManager.AddToRoleAsync(newUser, "Admin");
                      
                        return newUser;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
        public async Task<bool> CreateUserNoPasswordAsync(AppUserEntity newUser)
        {
            try
            {
                if (!await _UserRepository.Exists(x => x.Email == newUser.Email))
                {
                    var isCreated = await _userManager.CreateAsync(newUser);
                    if (isCreated.Succeeded)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }
        public async Task<AppUserEntity> UpdateUser(AppUserEntity newValues)
        {
            try
            {
                if (await _UserRepository.Exists(x => x.Id == newValues.Id))
                {
                    var updatedUser = await _UserRepository.UpdateEntityInDB(newValues, x => x.Id == newValues.Id);
                    if (updatedUser != null)
                        return updatedUser;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
        public async Task<bool> UpdateWithUserManagerAsync(AppUserEntity newValues)
        {
            try
            {
                if (await _UserRepository.Exists(x => x.Email == newValues.Email))
                {
                    var updatedUser = await _userManager.UpdateAsync(newValues);
                    if (updatedUser.Succeeded)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }
        public async Task<AppUserEntity> GetByEmailAsync(AppUserEntity externalUser)
        {
            try
            {
                if (await _UserRepository.Exists(x => x.Email == externalUser.Email))
                {
                    var user = await _userManager.FindByEmailAsync(externalUser.Email!);
                    if (user != null) return user;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
        public async Task<AppUserEntity> GetByEmailAsync(string email)
        {
            try
            {
                if (await _UserRepository.Exists(x => x.Email == email))
                {
                    var user = await _userManager.FindByEmailAsync(email!);
                    if (user != null) return user;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
