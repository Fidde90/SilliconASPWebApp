using Infrastructure.Models.Forms;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class ContactViewModel
    {
        public ContactFormModel Form { get; set; } = new();

        public List<string>? Services = ["Service 1", "Service 2", "Service 3", "Service 4", "Service 5"];
    }
}
