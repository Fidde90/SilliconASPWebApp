﻿using System.ComponentModel.DataAnnotations;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SignInViewModel
    {
        public string? Title { get; set; } = "Sign in";

        [Display(Name = "Email address", Prompt = "Enter your email address", Order = 0)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;

        [Display(Name = "Password", Prompt = "*******", Order = 1)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember me", Order = 2)]
        public bool Remember { get; set; } = false;

        public string? ErrorMessage { get; set; }

        public string? ReturnUrl { get; set; } = "/account";
    }
}
