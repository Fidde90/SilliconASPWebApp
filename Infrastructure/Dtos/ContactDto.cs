﻿namespace Infrastructure.Dtos
{
    public  class ContactDto
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Service { get; set; } = null!;

        public string Message { get; set; } = null!;
    }
}
