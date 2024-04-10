using Infrastructure.Dtos;
using Infrastructure.Models.Forms;

namespace Infrastructure.Factories
{
    public static class CategoryMapper
    {
        public static CreateCategoryDto ToCreateCategoryDto(CategoryFormModel model)
        {
            if (model != null)
            {
                var dto = new CreateCategoryDto
                {
                    CategoryName = model.CategoryName
                };

                return dto;
            }
            return null!;
        }
    }
}
