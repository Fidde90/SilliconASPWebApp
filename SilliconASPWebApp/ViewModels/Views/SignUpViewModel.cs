using SilliconASPWebApp.Models.Sections;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SignUpViewModel
    {
        public string Title { get; set; } = "Sign up";

        public SignUpFormModel From { get; set; } = new SignUpFormModel();


    }
}
