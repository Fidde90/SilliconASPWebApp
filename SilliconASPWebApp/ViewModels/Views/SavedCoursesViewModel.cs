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
        public ProfileMenuModel profileMenu {  get; set; } = new ProfileMenuModel(); 

        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
