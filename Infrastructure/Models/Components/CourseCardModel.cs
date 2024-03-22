﻿namespace SilliconASPWebApp.Models.Components
{
    public class CourseCardModel
    {
        public string PictureUrl { get; set; } = null!;
        
        public string Title { get; set; } = null!;

        public string? Author { get; set; }

        public string? Price { get; set; }

        public string? DiscountPrice { get; set; }

        public bool IsBestSeller { get; set; } = false;

        public string? LikesInNumbers { get; set; }

        public string? LikesInProcent { get; set; }

        public string Hours { get; set; } = null!;
    }
}
