namespace Infrastructure.Dtos
{
    public class CourseResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<CourseDto>? Courses { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public string? Category { get; set; }
    }
}
