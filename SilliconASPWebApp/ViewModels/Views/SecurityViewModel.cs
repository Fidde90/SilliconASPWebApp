using SilliconASPWebApp.Models.Forms;
using SilliconASPWebApp.Models.Sections;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SecurityViewModel
    {
        public string Title { get; set; } = "Security";

        public ProfileMenuModel ProfileMenuModel { get; set; } = new()
        {
            Image = new() { Src ="/images/John-doe.svg", AltText = "Profile picture" },
            Name = "John Doe",
            Email = "john.doe@hotmail.com"
        };
        public SecurityFormModel Form { get; set; } = new SecurityFormModel();

        public DeleteAccountFormModel DeleteForm { get; set; } = new DeleteAccountFormModel();

        public string ChangePasswordErrorMessage { get; set; } = string.Empty;
    
        public string DeleteAccountErrorMessage { get; set; } = string.Empty;
    }
}
