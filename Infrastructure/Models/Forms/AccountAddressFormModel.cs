using System.ComponentModel.DataAnnotations;

namespace SilliconASPWebApp.Models.Forms
{
    public class AccountAddressFormModel
    {
        [Display(Name = "Address line 1", Prompt = "Enter your address line", Order = 0)]
        [Required(ErrorMessage = "Adress is required")]
        public string Addressline_1 { get; set; } = null!;

        [Display(Name = "Address line 2", Prompt = "Enter your second address line", Order = 1)]
        public string? Addressline_2 { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal code", Prompt = "Enter your postal code", Order = 2)]
        [Required(ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; } = null!;

        [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = null!;
    }
}
