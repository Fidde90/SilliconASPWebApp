using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class SavedCoursesService
    {
       private readonly SavedCoursesRepository _savedCoursesRepository;

        public SavedCoursesService(SavedCoursesRepository savedCoursesRepository)
        {
            _savedCoursesRepository = savedCoursesRepository;
        }

        public async Task<bool> SaveCourseAsync(SavedCoursesEntity entity)
        {
            try
            {
                if(entity != null)
                {
                    if(! await _savedCoursesRepository.Exists(x => x.UserId == entity.UserId && x.CourseId == entity.Id))
                    {
                        var result = await _savedCoursesRepository.AddToDB(entity);
                        return true;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }

        public async Task<IEnumerable<CourseDto>> GetAllSavedCourses(string userId, List<CourseDto> dtoList)
        {
            List<CourseDto> returnList = new List<CourseDto>();
            try
            {
                var coursesList = await _savedCoursesRepository.GetAllFromDB();

                if (coursesList.ToList().Count > 0)
                {
                    for (int i = 0; i < coursesList.ToList().Count; i++)
                    {
                        if (coursesList.ToList()[i].UserId == userId)
                        {
                            returnList.Add(dtoList[i]);
                        }
                    }

                    return returnList;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
