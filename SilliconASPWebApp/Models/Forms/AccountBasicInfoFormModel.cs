using System.ComponentModel.DataAnnotations;

namespace SilliconASPWebApp.Models.Forms
{
    public class AccountBasicInfoFormModel
    {
        //[DataType(DataType.ImageUrl)]
        //public string? ProfileImg { get; set; }

        [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
        [Required(ErrorMessage = "Invalid first name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
        [Required(ErrorMessage = "Invalid last name")]
        public string LastName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
        [Required(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone", Prompt = "Enter your phone", Order = 3)]
        [Required(ErrorMessage = "phone is required")]
        public string Phone { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [Display(Name = "Bio", Prompt = "Add a short bio...", Order = 4)]
        public string? Bio { get; set; }
    }
}
