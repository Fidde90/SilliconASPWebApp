namespace Infrastructure.Dtos
{
    public  class ContactMessageDto
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Service { get; set; } = null!;

        public string Message { get; set; } = null!;
    }
}
