using SilliconASPWebApp.Models.Forms;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SecurityViewModel
    {
        public string Title { get; set; } = "Security";
        public SecurityFormModel Form { get; set; } = new SecurityFormModel();
        public DeleteAccountFormModel DeleteForm { get; set; } = new DeleteAccountFormModel();

        public string ChangePasswordErrorMessage { get; set; } = string.Empty;
    
        public string DeleteAccountErrorMessage { get; set; } = string.Empty;
    }
}
