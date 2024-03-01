using SilliconASPWebApp.Models.Forms;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SecurityViewModel
    {
        public string Title { get; set; } = "Security";
        public SecurityFormModel Form { get; set; } = new SecurityFormModel();

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
