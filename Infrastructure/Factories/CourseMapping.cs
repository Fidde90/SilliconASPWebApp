using Infrastructure.Dtos;

namespace Infrastructure.Factories
{
    public static class CourseMapping
    {
        public static UpdateCourseDto ToUpdateCourseDto(CourseDto dto)
        {
            if(dto != null)
            {
                var newDto = new UpdateCourseDto
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Author = dto.Author,
                    Price = dto.Price,
                    Hours = dto.Hours,
                    PictureUrl = dto.PictureUrl,
                    IsBestSeller = dto.IsBestSeller,
                    IsDigital = dto.IsDigital,
                    LikesInNumbers = dto.LikesInNumbers,
                    LikesInProcent = dto.LikesInProcent,
                    DiscountPrice = dto.DiscountPrice,    
                    LastUpdated = DateTime.Now,
                    Category = dto.Category
                };

                return newDto;
            }
            return null!;
        }
    }
}
