using SilliconASPWebApp.Models.Components;

namespace SilliconASPWebApp.Models.Sections.HomeSections
{
    public class FeaturesModel
    {
        public string? Title { get; set; } = null!;

        public string? Text { get; set; } = null!;

        public List<FeaturesBox> FeaturesBoxes { get; set; } = null!;
    }
}
