using SilliconASPWebApp.ViewModels.Sections;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class HomeIndexViewModel
    {
        public string Title = "Home";

        public ShowcaseViewModel Showcase = new ShowcaseViewModel().SetValues();
    }
}
