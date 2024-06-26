﻿using SilliconASPWebApp.Models.Components;

namespace SilliconASPWebApp.Models.Sections
{
    public class ProfileMenuModel
    {
        public string? Image { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public List<ProfileMenuLinkModel> Links = [
            new() { Link = new() { Controller = "Account", Action = "Details", Text = "Account details" }, Symbol="fa-regular fa-gear" },
            new() { Link = new() { Controller = "Account", Action = "Security", Text = "Security" }, Symbol = "fa-regular fa-lock" },
            new() { Link = new() { Controller = "Account", Action = "SavedCourses", Text = "Saved courses" }, Symbol = "fa-regular fa-bookmark" },
            new() { Link = new() { Controller = "Auth", Action = "SignOut", Text = "Sign out" }, Symbol = "fa-solid fa-right-from-bracket fa-flip-horizontal" }
        ];
    }
}
