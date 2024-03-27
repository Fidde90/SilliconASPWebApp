using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.Forms
{
    public class UpdateSubscriberFormModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
        public string Email { get; set; } = null!;
    }
}
