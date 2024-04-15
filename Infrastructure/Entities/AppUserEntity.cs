using Infrastructure.Dtos;
using Microsoft.AspNetCore.Identity;

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

        public virtual AddressEntity? Address { get; set; }

        [ProtectedPersonalData]
        public string? ProfilePicUrl { get; set; } = "~/uploads/doomguy.jpg";

        public bool IsExternal { get; set; } = false;
    }
}
