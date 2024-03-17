﻿using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities
{
    public class AppUserEntity : IdentityUser
    {
        [ProtectedPersonalData]
        public string FirstName { get; set; } = null!;

        [ProtectedPersonalData]
        public string LastName { get; set; } = null!;

        [ProtectedPersonalData]
        public string? Bio { get; set; }

        public int? AddressId { get; set; }

        public AddressEntity? Address { get; set; }

        [ProtectedPersonalData]
        public string? ProfilePicUrl { get; set; } = "/images/John-doe.svg";

        public bool IsExternal { get; set; } = false;
    }
}
