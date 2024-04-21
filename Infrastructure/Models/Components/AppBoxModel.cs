using SilliconASPWebApp.Models.Components;

namespace Infrastructure.Models.Components
{
    public class AppBoxModel
    {
        public ImageModel ImageModel { get; set; } = new();

        public string Text { get; set; } = null!;
    }
}
