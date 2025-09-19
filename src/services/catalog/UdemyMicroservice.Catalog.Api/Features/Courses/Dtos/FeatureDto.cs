namespace UdemyMicroservice.Catalog.Api.Features.Courses.Dtos
{
    public record FeatureDto(int Duration, float Rating, string EducatorFullName, DateTime CreatedAt, Guid CreatedBy, DateTime? UpdatedAt, Guid? UpdatedBy);
}