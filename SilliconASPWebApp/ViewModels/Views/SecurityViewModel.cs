using SilliconASPWebApp.Models.Forms;
using SilliconASPWebApp.Models.Sections;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SecurityViewModel
    {
        public string Title { get; set; } = "Security";

        public ProfileMenuModel ProfileMenuModel { get; set; } = new();

        public SecurityFormModel Form { get; set; } = new();

        public DeleteAccountFormModel DeleteForm { get; set; } = new();

        public string ChangePasswordErrorMessage { get; set; } = string.Empty;
    
        public string DeleteAccountErrorMessage { get; set; } = string.Empty;
    }
}
