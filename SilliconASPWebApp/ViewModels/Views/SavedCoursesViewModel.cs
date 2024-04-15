using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using SilliconASPWebApp.Models.Sections;
using System.Security.Claims;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class SavedCoursesViewModel
    {
        //private readonly UserManager<AppUserEntity> _userManager;
        //private readonly SavedCoursesService _SavedCourseService;


        //public SavedCoursesViewModel(UserManager<AppUserEntity> userManager, SavedCoursesService savedCourseService)
        //{
        //    _userManager = userManager;
        //    _SavedCourseService = savedCourseService;
        //}
        public SavedCoursesViewModel()
        {
            
        }

        public ProfileMenuModel profileMenu {  get; set; } = new ProfileMenuModel(); 

        public CourseResult CourseResult { get; set; } = new CourseResult();

        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();


        //public async Task<List<CourseDto>> FilterCourses(IEnumerable<CourseDto> courses, ClaimsPrincipal user)
        //{

        //    var currentUser = await _userManager.GetUserAsync(user);
        //    if (currentUser != null)
        //    {
   

        //        var listToReturn = await _SavedCourseService.GetAllSavedCourses(currentUser.Id, courses.ToList());

        //        return listToReturn.ToList();
        //    }

        //    return null!;

        //}
    }
}
