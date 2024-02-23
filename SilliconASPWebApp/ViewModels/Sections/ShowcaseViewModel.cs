using SilliconASPWebApp.Models.Components;
using SilliconASPWebApp.Models.Sections;

namespace SilliconASPWebApp.ViewModels.Sections
{
    public class ShowcaseViewModel
    {
        public string? Title { get; set; }

        public string? Text { get; set; }

        public LinkModel Link { get; set; } = new LinkModel();

        public string? BrandsTitle { get; set; }

        public ImageModel ShowcaseImage { get; set; } = null!;

        public List<ImageModel>? Brands { get; set; }
        public ShowcaseViewModel SetValues()
        {
            return new ShowcaseViewModel
            {
                Title = "Task Management Assistant You Gonna Love",
                Text = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.",
                Link = new LinkModel { Controller = "Home", Action = "Index", Text = "Get started for free" },
                BrandsTitle = "Largest companies use our tool to work efficiently",
                Brands = [
                    new() { Src = "/images/logoipsum.svg", AltText = "brand logo" },
                    new() { Src = "/images/logoipsum2.svg", AltText = "brand logo" },
                    new() { Src = "/images/logoipsum3.svg", AltText = "brand logo" },
                    new() { Src = "/images/logoipsum4.svg", AltText = "brand logo" },
                ],
                ShowcaseImage = new ImageModel() { Src = "/images/showcaseImage.svg", AltText = "Task master image" }
            };
        }
    }
}
