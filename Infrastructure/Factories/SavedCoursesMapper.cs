using Infrastructure.Entities;

namespace Infrastructure.Factories
{
    public static class SavedCoursesMapper
    {
        public static SavedCoursesEntity ToSavedCoursesEntity(int courseId, string userId)
        {
            if(!string.IsNullOrEmpty(userId) && !string.IsNullOrWhiteSpace(userId) && courseId >= 0)
            {
                var newSave = new SavedCoursesEntity
                {
                    CourseId = courseId,
                    UserId = userId
                };

                return newSave;
            }
            return null!;
        }
    }
}
