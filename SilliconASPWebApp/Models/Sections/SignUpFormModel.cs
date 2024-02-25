using SilliconASPWebApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SilliconASPWebApp.Models.Sections
{
    public class SignUpFormModel
    {
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

        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter your password", Order = 3)]
        [Required(ErrorMessage = "Invalid password")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password", Prompt = "Confirm password", Order = 4)]
        [Required(ErrorMessage = "Passwords must match")]
        [Compare(nameof(Password), ErrorMessage = "Passwords did not match!")]
        public string ConfirmPassword { get; set; } = null!;

        [Display(Name = "I agree to the Terms & Conditions", Order = 5)]
        [CheckBoxRequired(ErrorMessage = "You must accept the terms and conditions to proceed.")]
        public bool Terms { get; set; } = false;
    }
}
