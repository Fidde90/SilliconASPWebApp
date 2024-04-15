using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Dtos
{
    public class AppUserDto
    {

        public string FirstName { get; set; } = null!;


        public string LastName { get; set; } = null!;


        public string? Bio { get; set; }

        public int? AddressId { get; set; }

        public AddressEntity? Address { get; set; }


        public string? ProfilePicUrl { get; set; } = "/uploads/doomguy.jpg";

        public bool IsExternal { get; set; } = false;

        public virtual ICollection<CourseDto> PlayerWeapons { get; set; } = new List<CourseDto>();
    }
}
