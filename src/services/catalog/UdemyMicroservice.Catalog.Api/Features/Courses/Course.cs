﻿using UdemyMicroservice.Catalog.Api.Data;
using UdemyMicroservice.Catalog.Api.Features.Categories;

namespace UdemyMicroservice.Catalog.Api.Features.Courses
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public Feature Feature { get; set; } = default!;
    }
}