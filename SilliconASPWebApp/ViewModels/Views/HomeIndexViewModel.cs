using Infrastructure.Models.Forms;
using Infrastructure.Models.Sections.HomeSections;
using SilliconASPWebApp.Models.Components;
using SilliconASPWebApp.Models.Sections;
using SilliconASPWebApp.Models.Sections.HomeSections;


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

        public WorkToolsModel Tools = new()
        {
            Title = "Integrate top work tools",
            Paragraph = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin volutpat mollis egestas. Nam luctus facilisis ultrices. Pellentesque volutpat ligula est. Mattis fermentum, at nec lacus.",
            AppBoxModels =
                  [
                    new(){ ImageModel={Src="\\images\\googlelogo.svg", AltText="google logo" }, Text="Lorem magnis pretium sed curabitur nunc facilisi nunc cursus sagittis." },
                    new(){ ImageModel={Src="\\images\\kameralogo.svg", AltText = "Zoom logo"}, Text="In eget a mauris quis. Tortor dui tempus quis integer est sit natoque placerat dolor." },
                    new(){ ImageModel={Src="\\images\\colorfullogo.svg", AltText="Stack logo" }, Text="Id mollis consectetur congue egestas egestas suspendisse blandit justo." },
                    new(){ ImageModel={Src="\\images\\envelope.svg", AltText="Gmail logo" }, Text="Rutrum interdum tortor, sed at nulla. A cursus bibendum elit purus cras praesent." },
                    new(){ ImageModel={Src="\\images\\tabletlogo.svg", AltText="Trello logo" }, Text="Congue pellentesque amet, viverra curabitur quam diam scelerisque fermentum urna." },
                    new(){ ImageModel={Src="\\images\\monkeylogo.svg", AltText="Mailchimp logo" }, Text="A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris." },
                    new(){ ImageModel={Src="\\images\\boxlogo.svg", AltText="Dropbox logo" }, Text="Ut in turpis consequat odio diam lectus elementum. Est faucibus blandit platea." },
                    new(){ ImageModel={Src="\\images\\elephantlogo.svg", AltText= "Evernote logo" }, Text="Faucibus cursus maecenas lorem cursus nibh. Sociis sit risus id. Sit facilisis dolor arcu" }
                 ]
        };

        public SubscribeFormModel SubscribeModel = new();
    }
}
