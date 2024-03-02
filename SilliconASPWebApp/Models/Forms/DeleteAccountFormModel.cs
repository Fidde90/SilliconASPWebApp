using SilliconASPWebApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SilliconASPWebApp.Models.Forms
{
    public class DeleteAccountFormModel
    {

        [Display(Name = " Yes, I want to delete my account.", Order = 0)]
        [CheckBoxRequired(ErrorMessage = "You must confirm")]
        public bool Delete { get; set; } = false;
    }
}
