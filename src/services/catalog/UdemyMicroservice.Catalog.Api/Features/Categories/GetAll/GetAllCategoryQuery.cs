namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public record GetAllCategoryQuery : IRequestByServiceResult<IEnumerable<CategoryDto>>;
}