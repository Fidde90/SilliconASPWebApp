using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AppUserService
    {
        private readonly UserManager<AppUserEntity> _userManager;
        private readonly AppUserRepository _appUserRepository;

        public AppUserService(UserManager<AppUserEntity> userManager, AppUserRepository appUserRepository)
        {
            _userManager = userManager;
            _appUserRepository = appUserRepository;
        }

        public async Task<bool> CreateUser(AppUserEntity user)
        {
            if (!await _appUserRepository.Exists(x => x.Id == user.Id))
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash!);
                if (result.Succeeded)
                    return true;
            }
            return false;
        }
    }
}
