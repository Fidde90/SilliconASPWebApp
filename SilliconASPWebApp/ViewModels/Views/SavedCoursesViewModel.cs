using Infrastructure.Dtos;
using SilliconASPWebApp.Models.Sections;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SavedCoursesViewModel
    {
        public ProfileMenuModel profileMenu {  get; set; } = new ProfileMenuModel(); 

        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
