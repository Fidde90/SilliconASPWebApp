using Infrastructure.Models.Components;

namespace Infrastructure.Models.Sections.HomeSections
{
    public class WorkToolsModel
    {
        public string Title { get; set; } = null!;

        public string Paragraph { get; set; } = null!;

        public List<AppBoxModel> AppBoxModels { get; set; } = new();
    }
}
