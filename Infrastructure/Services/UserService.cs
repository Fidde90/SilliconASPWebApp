﻿using Infrastructure.Entities;
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

        public async Task<bool> CreateUser(AppUserEntity newUser, string password)
        {
            try
            {
                if (!await _UserRepository.Exists(x => x.Email == newUser.Email))
                {
                    var result = await _userManager.CreateAsync(newUser, password);
                    if (result.Succeeded)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }
    }
}