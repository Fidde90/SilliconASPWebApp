using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.Forms
{
    public class ContactFormModel
    {
        [Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
        [Required(ErrorMessage = "Invalid name")]
        [MinLength(2, ErrorMessage = "Invalid name")]
        public string FullName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
        [Required(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Service", Order = 2)]
        [MinLength(2, ErrorMessage = "Invalid first name")]
        public string Service { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [Display(Name = "Message", Prompt = "Enter your message...", Order = 3)]
        [Required(ErrorMessage = "Message is required")]
        [MinLength(10, ErrorMessage = "The message must contain at least 10 letters")]
        [MaxLength(1000)]
        public string Message { get; set; } = null!;
    }
}
