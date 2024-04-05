using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.Forms
{
    public class CategoryFormModel
    {
        [DataType(DataType.Text)]
        [Display(Prompt = "Enter a category name")]
        public string CategoryName { get; set; } = null!;
    }
}
