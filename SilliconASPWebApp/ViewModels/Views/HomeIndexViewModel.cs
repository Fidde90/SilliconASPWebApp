using SilliconASPWebApp.Models.Components;
using SilliconASPWebApp.Models.Sections;


namespace SilliconASPWebApp.ViewModels.Views
{
    public class HomeIndexViewModel
    {
        public string Title = "Home";

        public ShowcaseModel Showcase = new()
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

        public FeaturesModel Features = new() 
        {
            Title = "What Do You Get with Our Tool?",
            Text = "Make sure all your tasks are organized so you can set the priorities and focus on important.",
            FeaturesBoxes =
                [
                    new() { BoxClass = "box-content", Image = new() { Src = "/images/chat-bubbles.svg", AltText = "chat-bubbles" }, BoxTitle = "Comments on Tasks", BoxText = "Id mollis consectetur congue egestas egestas suspendisse blandit justo." },
                    new() { BoxClass = "box-content middlebox", Image = new() { Src = "/images/computerscreen.svg", AltText = "monitor image" }, BoxTitle = "Tasks Analytics", BoxText = "Non imperdiet facilisis nulla tellus Morbi scelerisque eget adipiscing vulputate." },
                    new() { BoxClass = "box-content", Image = new() { Src = "/images/vectorWithPlus.svg", AltText = "users" }, BoxTitle = "Multiple Assignees", BoxText = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris." },
                    new() { BoxClass = "box-content bottom", Image = new() { Src = "/images/bell.svg", AltText = "bell image" }, BoxTitle = "Notifications", BoxText = "Diam, suspendisse velit cras ac. Lobortis diam volutpat, eget pellentesque viverra." },
                    new() { BoxClass = "box-content bottom-middlebox", Image = new() { Src = "/images/tasks.svg", AltText = "tasklist image" }, BoxTitle = "Sections & Subtasks", BoxText = "Mi feugiat hac id in. Sit elit placerat lacus nibh lorem ridiculus lectus." },
                    new() { BoxClass = "box-content bottom", Image = new() { Src = "/images/shield.svg", AltText = "shield image" }, BoxTitle = "Data Security", BoxText = "Aliquam malesuada neque eget elit nulla vestibulum nunc cras." }
               ]
        };

    }
}
