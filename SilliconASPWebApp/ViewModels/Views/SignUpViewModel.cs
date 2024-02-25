using SilliconASPWebApp.Models.Sections;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SignUpViewModel
    {
        public string Title { get; set; } = "Sign up";

        public SignUpFormModel Form { get; set; } = new SignUpFormModel();
    }
}
