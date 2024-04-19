using Infrastructure.Dtos;
using SilliconASPWebApp.Models.Components;

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

        public static UpdateCourseDto ToUpdateCourseDto(CourseCardModel model)
        {
            if (model != null)
            {
                var newDto = new UpdateCourseDto
                {
                    Id = model.Id,
                    Title = model.Title,
                    Author = model.Author,
                    Price = model.Price,
                    Hours = model.Hours,
                    PictureUrl = model.PictureUrl,
                    IsDigital = model.IsDigital,
                    IsBestSeller = model.IsBestSeller,
                    LikesInNumbers = model.LikesInNumbers,
                    LikesInProcent = model.LikesInProcent,
                    DiscountPrice = model.DiscountPrice,
                    LastUpdated = DateTime.Now,
                    Category = model.Category!
                };

                return newDto;
            }
            return null!;
        }

        public static CourseCardModel ToCourseCardModel(CourseDto dto)
        {
            if (dto != null)
            {
                var newCardModel = new CourseCardModel
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Author = dto.Author,
                    Price = dto.Price,
                    Hours = dto.Hours!,
                    PictureUrl = dto.PictureUrl!,
                    IsBestSeller = dto.IsBestSeller,
                    IsDigital = dto.IsDigital,
                    LikesInNumbers = dto.LikesInNumbers,
                    LikesInProcent = dto.LikesInProcent,
                    DiscountPrice = dto.DiscountPrice,
                    Category = dto.Category 
                };

                return newCardModel;
            }
            return null!;
        }

        public static CourseCardModel ToCourseCardModel(UpdateCourseDto dto)
        {
            if (dto != null)
            {
                var newCardModel = new CourseCardModel
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Author = dto.Author,
                    Price = dto.Price,
                    Hours = dto.Hours!,
                    PictureUrl = dto.PictureUrl!,
                    IsBestSeller = dto.IsBestSeller,
                    IsDigital = dto.IsDigital,
                    LikesInNumbers = dto.LikesInNumbers,
                    LikesInProcent = dto.LikesInProcent,
                    DiscountPrice = dto.DiscountPrice,
                    Category = dto.Category
                };

                return newCardModel;
            }
            return null!;
        }

        public static CourseDto ToCourseDto(CourseCardModel model)
        {
            if (model != null)
            {
                var newDto = new CourseDto
                {
                    Id = model.Id,
                    Title = model.Title,
                    Author = model.Author,
                    Price = model.Price,
                    Hours = model.Hours,
                    PictureUrl = model.PictureUrl,
                    IsBestSeller = model.IsBestSeller,
                    IsDigital = model.IsDigital,
                    LikesInNumbers = model.LikesInNumbers,
                    LikesInProcent = model.LikesInProcent,
                    DiscountPrice = model.DiscountPrice,
                    Category = model.Category!

                };

                return newDto;
            }
            return null!;
        }

        public static CourseDto ToCourseDto(UpdateCourseDto dto)
        {
            if (dto != null)
            {
                var newDto = new CourseDto
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
                    Category = dto.Category
                };

                return newDto;
            }
            return null!;
        }
    }
}
