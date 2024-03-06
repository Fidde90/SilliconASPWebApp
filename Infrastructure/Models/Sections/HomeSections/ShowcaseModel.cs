using SilliconASPWebApp.Models.Components;

namespace SilliconASPWebApp.Models.Sections
{
    public class ShowcaseModel
    {
        public string? Title { get; set; } 
       
        public string? Text { get; set; } 
        
        public LinkModel Link { get; set; } = new LinkModel();

        public string? BrandsTitle { get; set; }

        public ImageModel ShowcaseImage { get; set; } = null!;

        public List<ImageModel>? Brands {  get; set; }
    }
}
