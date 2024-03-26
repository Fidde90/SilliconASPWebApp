﻿using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AccountService
    {
        private readonly UserManager<AppUserEntity> _userManager;
        private readonly UserRepository _userRepository;

        public AccountService(UserManager<AppUserEntity> userManager, UserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }
    }
}
