using SilliconASPWebApp.Models.Sections;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SignInViewModel
    {
        public string Title { get; set; } = "Sign in";

        public SignInFormModel Form { get; set; } = new SignInFormModel();

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
