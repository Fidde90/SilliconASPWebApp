namespace Infrastructure.Dtos
{
    public class PaginationDto
    {
        public int CurrentPage { get; set; }
        public int TotalPages {  get; set; }
        public int PageSize {  get; set; }
        public int TotalItems { get; set; }
        public string? Category { get; set; }
        public void UpdateTotalPages()
        {
            TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
        }
    }
}
