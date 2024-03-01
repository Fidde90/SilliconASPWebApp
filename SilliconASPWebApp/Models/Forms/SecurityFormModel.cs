using System.ComponentModel.DataAnnotations;

namespace SilliconASPWebApp.Models.Forms
{
    public class SecurityFormModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Current password", Prompt = "Enter your current password", Order = 0)]
        [Required(ErrorMessage = "Invalid password")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "New password", Prompt = "Enter new password", Order = 1)]
        [Required(ErrorMessage = "Invalid Password")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password")]
        public string NewPassword { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password", Prompt = "Confirm password", Order = 2)]
        [Required(ErrorMessage = "Passwords must match")]
        [Compare(nameof(Password), ErrorMessage = "Passwords did not match!")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
