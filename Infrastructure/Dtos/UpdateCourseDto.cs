namespace Infrastructure.Dtos
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public string? Author { get; set; }

        public string? Price { get; set; }

        public string? DiscountPrice { get; set; }

        public string? IsBestSeller { get; set; }

        public string? LikesInNumbers { get; set; }

        public string? LikesInProcent { get; set; }

        public string Hours { get; set; } = null!;
    }
}
