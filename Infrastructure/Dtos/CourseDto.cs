namespace Infrastructure.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Author { get; set; }

        public string? Price { get; set; }

        public string? Hours { get; set; }

        public string? PictureUrl { get; set; }

        public bool IsBestSeller { get; set; } = false;

        public string? LikesInNumbers { get; set; }

        public string? LikesInProcent { get; set; }

        public string? DiscountPrice { get; set; }

        public CategoryDto? Category { get; set; }
    }
}
