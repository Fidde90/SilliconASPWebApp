using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class AccountService(UserManager<AppUserEntity> userManager, IConfiguration configuration, DataContext dataContext)
    {
        private readonly UserManager<AppUserEntity> _userManager = userManager;
        private readonly DataContext _dataContext = dataContext;
        private readonly IConfiguration _configuration = configuration;

        public async Task<bool> UploadProfileImgAsync(ClaimsPrincipal user, IFormFile file)
        {
            try
            {
                if (user != null && file != null && file.Length > 0)
                {
                    var entity = await _userManager.GetUserAsync(user);
                    if (entity != null)
                    {
                        var fileName = $"p_{entity.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FileUploadPath"]!, fileName);

                        using var fs = new FileStream(filePath, FileMode.Create);
                        await file.CopyToAsync(fs);

                        entity.ProfilePicUrl = fileName;
                        _dataContext.Update(entity);
                        await _dataContext.SaveChangesAsync();

                        return true;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: {0}", e.Message); }
            return false;
        }
    }
}
