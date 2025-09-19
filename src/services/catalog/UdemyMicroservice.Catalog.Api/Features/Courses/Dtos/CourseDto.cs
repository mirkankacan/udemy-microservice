namespace UdemyMicroservice.Catalog.Api.Features.Courses.Dtos
{
    public record CourseDto(Guid Id, string Description, string ImageUrl, string Name, decimal Price, Guid UserId, CategoryDto Category, FeatureDto Feature);
}