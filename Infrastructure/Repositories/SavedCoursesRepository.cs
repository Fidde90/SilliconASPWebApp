using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SavedCoursesRepository : BaseRepository<SavedCoursesEntity> 
    {
        private readonly DataContext _dataContext;
        public SavedCoursesRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        //public async Task<List<CourseDto>> GetSavedCourses(string userId, int courseId)
        //{
        //    List<CourseDto> list = new List<CourseDto>();


        //    var query = await _dataContext.SavedCourses.ToListAsync().AsQueryable(); //inkluderar categorierna och gör den frågbar

        //    for (int i = 0; i < _dataContext.SavedCourses.ToList().Count - 1; i++)
        //    {
        //        query = query.Where(x => x.UserId == userId);
        //    }

        //    var courses = await query.ToListAsync();
        //    return list;
        //}
    }
}
