using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.Forms
{
    public class SubscribeFormModel
    {
        [Required(ErrorMessage = "Enter a vaild email")]
        [DataType(DataType.EmailAddress)]
        [Display(Prompt = "Your Email")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
        public string Email { get; set; } = null!;
    }
}
