using SilliconASPWebApp.Models.Components;
using SilliconASPWebApp.Models.Sections;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SavedCoursesViewModel
    {
        public ProfileMenuModel ProfileMenuModel { get; set; } = new (){
                Image = "images/John-doe.svg",
                Name = "John Doe",
                Email ="john.doe@hotmail.com"   
        };

        public CourseCardModel CourseCard = new()
        {
            Image = new() { Src = "/images/course-jmetter.svg", AltText = "blablabla" },
            Title = "något",
            Teacher = "någon",
            Price = 38,
            Hours = "150h",
            rates = "50% 2k"
        };
    }
}
