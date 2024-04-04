using Infrastructure.Dtos;
using SilliconASPWebApp.Models.Components;

namespace Infrastructure.Factories
{
    public static class CourseMapping
    {
        public static UpdateCourseDto ToUpdateCourseDto(CourseCardModel model)
        {
            if(model != null)
            {
                var newModel = new UpdateCourseDto
                {
                    Id = model.Id,
                    Title = model.Title,
                    PictureUrl = model.PictureUrl,
                    Author = model.Author,
                    Price = model.Price,
                    DiscountPrice = model.DiscountPrice,
                    IsBestSeller = model.IsBestSeller.ToString(),
                    LikesInNumbers = model.LikesInNumbers,
                    LikesInProcent = model.LikesInProcent,
                    Hours = model.Hours
                };

                return newModel;
            }
            return null!;
        }
    }
}
