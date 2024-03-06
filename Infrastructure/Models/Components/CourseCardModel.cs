using Microsoft.AspNetCore.Mvc.Formatters;

namespace SilliconASPWebApp.Models.Components
{
    public class CourseCardModel
    {
        public ImageModel Image { get; set; } = null!;
        
        public string Title { get; set; } = null!;
        
        public string Teacher { get; set; } = null!;
        
        public decimal Price { get; set; }
        
        public string Hours { get; set; } = null!;

        public string rates { get; set; } = null!;

    }
}
