using Infrastructure.Dtos;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class CoursesIndexViewModel
    {
        public IEnumerable<CategoryDto>? Categories;

        public IEnumerable<CourseDto>? Courses;
    }
}
 